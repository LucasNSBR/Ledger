using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Commands.RoleCommands;
using Ledger.Identity.Domain.Models.Services.UserServices;
using Ledger.Identity.Domain.Services.RoleServices;
using Ledger.Shared.IntegrationEvents.Events.RoleEvents;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.Identity.Application.AppServices.RoleAppServices
{
    public class RoleApplicationService : BaseApplicationService, IRoleApplicationService
    {
        private readonly LedgerRoleManager _roleManager;
        private readonly LedgerUserManager _userManager;

        public RoleApplicationService(LedgerRoleManager roleManager, LedgerUserManager userManager, IDomainNotificationHandler domainNotificationHandler, IDomainServiceBus domainServiceBus, IIntegrationServiceBus integrationBus) : base(domainNotificationHandler, domainServiceBus, integrationBus)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IQueryable<LedgerIdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<LedgerIdentityRole> GetByName(string name)
        {
            LedgerIdentityRole role = await _roleManager.FindByNameAsync(name);
            return role;
        }

        public async Task Register(RegisterRoleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityRole role = new LedgerIdentityRole(command.Name);

            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                await Publish(new RoleRegisteredIntegrationEvent(role.Id, role.Name));
            else
                AddNotifications(result);
        }

        public async Task Remove(RemoveRoleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            LedgerIdentityRole role = await GetByName(command.RoleName);

            if (NotifyNullRole(role))
                return;

            bool canDelete = await CanDeleteRole(role);
            if (!canDelete)
            {
                AddNotification("Falha ao remover", "Não é possível remover essa role enquanto ouver usuários nela.");
                return;
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                await Publish(new RoleRemovedIntegrationEvent(role.Name));
            else
                AddNotifications(result);
        }

        private bool NotifyNullRole(LedgerIdentityRole role)
        {
            if (role == null)
            {
                AddNotification("Role inválida", "Não existe uma role com esse nome.");
                return true;
            }

            return false;
        }

        private async Task<bool> CanDeleteRole(LedgerIdentityRole role)
        {
            return !(await _userManager.GetUsersInRoleAsync(role.Name)).Any();
        }
    }
}
