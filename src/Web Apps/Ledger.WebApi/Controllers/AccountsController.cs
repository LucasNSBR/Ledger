using Ledger.CrossCutting.Identity.Abstractions;
using Ledger.CrossCutting.Identity.Commands;
using Ledger.CrossCutting.Identity.Models.Services;
using Ledger.CrossCutting.Identity.Models.Users;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : BaseController
    {
        private readonly LedgerUserManager _userManager;
        private readonly LedgerSignInManager _signInManager;
        private readonly IJwtFactory _jwtFactory;

        public AccountsController(IDomainNotificationHandler domainNotificationHandler, LedgerSignInManager signInManager, LedgerUserManager userManager, IJwtFactory jwtFactory)
                                                                                                                            : base(domainNotificationHandler)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _signInManager = signInManager;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            command.Validate();

            if (command.HasNotifications())
                CreateResponse();

            LedgerIdentityUser user = new LedgerIdentityUser(command.Email);

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);
            if (result.Succeeded)
            {
                string confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //TODO: ADD E-MAIL SERVICE

                return CreateResponse(new
                {
                    email = user.Email,
                });
            }
            else
                AddIdentityErrors(result);

            return CreateResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
        {
            command.Validate();

            if (command.HasNotifications())
                CreateResponse();

            LedgerIdentityUser user = await _userManager.FindByEmailAsync(command.Email);

            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, true);
                if (result.Succeeded)
                {
                    IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                    GenericIdentity identity = new GenericIdentity(user.Id.ToString());
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(identity, claims);

                    string token = _jwtFactory.WriteToken(user.Id.ToString(), claims);
                 
                    return CreateResponse(token);
                }
            }

            return CreateErrorResponse("Usuário e/ou senha estão incorretos.");
        }

        [HttpPost]
        [Route("recoverpassword")]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            LedgerIdentityUser user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                string recoverCode = await _userManager.GeneratePasswordResetTokenAsync(user);

                //TODO: ADD E-MAIL SERVICE

                CreateResponse();
            }

            return CreateErrorResponse("Não existe nenhum usuário registrado com esse e-mail.");
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordCommand command)
        {
            command.Validate();

            if (command.HasNotifications())
                CreateResponse();

            LedgerIdentityUser user = await _userManager.FindByEmailAsync(command.Email);

            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, command.ResetToken, command.NewPassword);
                if (!result.Succeeded)
                    AddIdentityErrors(result);

                return CreateResponse();
            }

            return CreateErrorResponse("Não existe nenhum usuário registrado com esse e-mail.");
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(Guid id)
        {
            LedgerIdentityUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                //TODO: REVOKE JWT TOKEN
            }

            return CreateResponse();
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                AddNotification(error.Code, error.Description);
            }
        }
    }
}