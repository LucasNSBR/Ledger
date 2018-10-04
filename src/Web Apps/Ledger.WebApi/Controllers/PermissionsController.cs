using Ledger.Identity.Application.AppServices.RoleAppServices;
using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Commands.RoleCommands;
using Ledger.Identity.Domain.Commands.UserCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/permissions")]
    //[Authorize(Policy = "AdminAccount")]
    public class PermissionsController : BaseController
    {
        private readonly IRoleApplicationService _roleApplicationService;
        private readonly IUserApplicationService _userApplicationService;
        
        public PermissionsController(IRoleApplicationService roleApplicationService, IUserApplicationService userApplicationService, IDomainNotificationHandler domainNotificationHandler) 
                                                                                                                                                          : base(domainNotificationHandler)
        {
            _roleApplicationService = roleApplicationService;
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        [Route("throw")]
        public IActionResult Throw()
        {
            throw new System.Exception("Erro fatal na aplicação");
        }

        [HttpGet]
        [Route("roles")]
        public IActionResult GetAllRoles()
        {
            IQueryable<LedgerIdentityRole> roles = _roleApplicationService.GetAllRoles();

            return CreateResponse(roles);
        }

        [HttpGet]
        [Route("roles/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            LedgerIdentityRole role = await _roleApplicationService.GetByName(name);

            return CreateResponse(role);
        }

        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> Register([FromBody]RegisterRoleCommand command)
        {
            await _roleApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpDelete]
        [Route("roles/{name}")]
        public async Task<IActionResult> Remove(string name, [FromBody]RemoveRoleCommand command)
        {
            command.RoleName = name;

            await _roleApplicationService.Remove(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("users/{email}/roles")]
        public async Task<IActionResult> AddToRole(string email, [FromBody]AddUserToRoleCommand command)
        {
            command.Email = email;

            await _userApplicationService.AddToRole(command);

            return CreateResponse();
        }

        [HttpDelete]
        [Route("users/{email}/roles")]
        public async Task<IActionResult> RemoveFromRole(string email, [FromBody]RemoveUserFromRoleCommand command)
        {
            command.Email = email;

            await _userApplicationService.RemoveFromRole(command);

            return CreateResponse();
        }
    }
}