using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Domain.Commands;
using Ledger.Identity.Domain.Models.Aggregates.UserAggregate.User;
using Ledger.Identity.Domain.Services;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : BaseController
    {
        private readonly IUserApplicationService _userApplicationService;
        private readonly IJwtFactory _jwtFactory;

        public AccountsController(IDomainNotificationHandler domainNotificationHandler, IUserApplicationService userApplicationService, IJwtFactory jwtFactory)
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
                return CreateResponse(token);
            }

            return CreateResponse();
        }

        [HttpPost]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmUserEmailCommand command)
        {
            await _userApplicationService.ConfirmEmail(command);

            return CreateResponse();
        }

        [HttpPost]
        [Authorize]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command)
        {
            await _userApplicationService.ChangePassword(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("recoverpassword")]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            ForgotUserPasswordCommand command = new ForgotUserPasswordCommand
            {
                Email = email
            };

            await _userApplicationService.ForgotPassword(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordCommand command)
        {
            await _userApplicationService.ResetPassword(command);

            return CreateResponse();
        }
    }
}