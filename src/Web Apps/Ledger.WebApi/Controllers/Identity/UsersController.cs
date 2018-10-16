using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers.Identity
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserApplicationService _userApplicationService;

        public UsersController(IUserApplicationService userApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllUsers()
        {
            IQueryable<LedgerIdentityUser> users = _userApplicationService.GetAllUsers();

            return CreateResponse(users.Select(user => new  
            {
                id = user.Id,
                email = user.Email,
                userName = user.UserName,
            }));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            LedgerIdentityUser user = await _userApplicationService.GetById(id);

            return CreateResponse(new
            {
                id = user.Id,
                email = user.Email,
                userName = user.UserName,
            });
        }
    }
}