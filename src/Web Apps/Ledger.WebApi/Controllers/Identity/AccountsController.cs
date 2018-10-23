using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.Domain.Commands.UserCommands;
using Ledger.Identity.Domain.Services;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers.Identity
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : BaseController
    {
        private readonly IUserApplicationService _userApplicationService;
        private readonly IJwtFactory _jwtFactory;

        public AccountsController(IUserApplicationService userApplicationService, IJwtFactory jwtFactory, IDomainNotificationHandler domainNotificationHandler)
                                                                                                                                : base(domainNotificationHandler)
        {
            _userApplicationService = userApplicationService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            LedgerIdentityUser user = await _userApplicationService.Register(command);

            if (user != null)
            {
                return CreateResponse(new
                {
                    id = user.Id,
                    email = user.Email
                });
            }

            return CreateResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
        {
            ClaimsIdentity identity = await _userApplicationService.Login(command);

            if (identity != null)
            {
                string token = _jwtFactory.WriteToken(identity);
                return CreateResponse(new
                {
                    id = identity.Name,
                    email = command.Email,
                    token 
                });
            }

            return CreateResponse();
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmUserEmailCommand command)
        {
            await _userApplicationService.ConfirmEmail(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotUserPasswordCommand command)
        {
            await _userApplicationService.ForgotPassword(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordCommand command)
        {
            await _userApplicationService.ResetPassword(command);

            return CreateResponse();
        }

        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command)
        {
            await _userApplicationService.ChangePassword(command);

            return CreateResponse();
        }
    }
}