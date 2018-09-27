using Ledger.Identity.Application.AppServices.RoleAppServices;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Commands.RoleCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/permissions")]
    public class PermissionsController : BaseController
    {
        private readonly IRoleApplicationService _roleApplicationService;
        
        public PermissionsController(IRoleApplicationService roleApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _roleApplicationService = roleApplicationService;
        }

        [HttpGet]
        [Route("roles")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult GetRoles()
        {
            IQueryable<LedgerIdentityRole> roles = _roleApplicationService.GetAllRoles();

            return CreateResponse(roles);
        }

        [HttpGet]
        [Route("roles/{name}")]
        //[Authorize(Policy = "AdminAccount")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            LedgerIdentityRole role = await _roleApplicationService.GetByName(name);

            return CreateResponse(role);
        }

        [HttpPost]
        [Route("roles")]
        //[Authorize(Policy = "AdminAccount")]
        public async Task<IActionResult> Register([FromBody]RegisterRoleCommand command)
        {
            await _roleApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpDelete]
        [Route("roles")]
        //[Authorize(Policy = "AdminAccount")]
        public async Task<IActionResult> Remove([FromBody]RemoveRoleCommand command)
        {
            await _roleApplicationService.Remove(command);

            return CreateResponse();
        }
    }
}