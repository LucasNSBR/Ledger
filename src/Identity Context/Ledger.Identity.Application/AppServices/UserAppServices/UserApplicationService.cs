using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Identity.Domain.Commands;
using Ledger.Identity.Domain.Events.UserEvents;
using Ledger.Identity.Domain.Models.Aggregates.UserAggregate.User;
using Ledger.Identity.Domain.Models.Services.UserServices;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Ledger.Identity.Application.AppServices.UserAppServices
{
    public class UserApplicationService : BaseApplicationService, IUserApplicationService
    {
        private readonly LedgerUserManager _userManager;
        private readonly LedgerSignInManager _signInManager;

        public UserApplicationService(LedgerUserManager userManager, LedgerSignInManager signInManager, IDomainNotificationHandler domainNotificationHandler, IDomainServiceBus domainServiceBus)
                                                                                                                                : base(domainNotificationHandler, domainServiceBus)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LedgerIdentityUser> GetById(Guid id)
        {
            LedgerIdentityUser user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<LedgerIdentityUser> GetByEmail(string email)
        {
            LedgerIdentityUser user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<LedgerIdentityUser> Register(RegisterUserCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return null;

            LedgerIdentityUser user = new LedgerIdentityUser(command.Email);

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);
            if (result.Succeeded)
            {
                string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await Publish(new UserRegisteredEvent(user.Email, confirmationToken));

                return user;
            }

            AddNotifications(result);
            return null;
        }

        public async Task<ClaimsIdentity> Login(LoginUserCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return null;

            LedgerIdentityUser user = await GetByEmail(command.Email);

            if (NotifyNullUser(user))
                return null;

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, true);
            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                GenericIdentity identity = new GenericIdentity(user.Id.ToString());
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(identity, claims);

                await Publish(new UserLoggedInEvent(user.Id, user.Email));

                return claimsIdentity;
            }

            AddNotification("Usuário inválido", "O usuário e/ou senha estão incorretos.");
            return null;
        }

        public async Task ConfirmEmail(ConfirmUserEmailCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityUser user = await GetByEmail(command.Email);

            if (NotifyNullUser(user))
                return;

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, command.ConfirmationToken);

            if (!result.Succeeded)
                AddNotifications(result);
            else
            {
                Claim activatedAccountClaim = new Claim("activated-account", "true");
                IdentityResult claimResult = await _userManager.AddClaimAsync(user, activatedAccountClaim);

                if (!claimResult.Succeeded)
                    AddNotifications(claimResult);
                else
                    await Publish(new UserEmailConfirmedEvent(user.Email));
            }
        }

        public async Task ChangePassword(ChangeUserPasswordCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityUser user = await GetByEmail(command.Email);

            if (NotifyNullUser(user))
                return;

            IdentityResult result = await _userManager.ChangePasswordAsync(user, command.Password, command.NewPassword);

            if (!result.Succeeded)
                AddNotifications(result);
            else
                await Publish(new UserChangedPasswordEvent(user.Email));
        }

        public async Task ForgotPassword(ForgotUserPasswordCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityUser user = await GetByEmail(command.Email);

            if (NotifyNullUser(user))
                return;

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            await Publish(new UserForgotPasswordEvent(user.Email, resetToken));
        }

        public async Task ResetPassword(ResetUserPasswordCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityUser user = await GetByEmail(command.Email);

            if (NotifyNullUser(user))
                return;

            IdentityResult result = await _userManager.ResetPasswordAsync(user, command.ResetToken, command.NewPassword);

            if (!result.Succeeded)
                AddNotifications(result);
            else
                await Publish(new UserResetedPasswordEvent(user.Email));
        }

        private bool NotifyNullUser(LedgerIdentityUser user)
        {
            if (user == null)
            {
                AddNotification("Erro ao encontrar usuário", "Não existe nenhum usuário registrado com esse e-mail.");
                return true;
            }

            return false;
        }
    }
}
