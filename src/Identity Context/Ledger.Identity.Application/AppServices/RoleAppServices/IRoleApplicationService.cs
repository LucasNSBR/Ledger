using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Commands.RoleCommands;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.Identity.Application.AppServices.RoleAppServices
{
    public interface IRoleApplicationService
    {
        IQueryable<LedgerIdentityRole> GetAllRoles();
        Task<LedgerIdentityRole> GetByName(string name);
        Task AddRole(RegisterRoleCommand command);
        Task RemoveRole(RemoveRoleCommand command);
    }
}
