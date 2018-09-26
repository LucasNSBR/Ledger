using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using System;

namespace Ledger.HelpDesk.Application.AppServices.RoleAppServices
{
    public interface IRoleApplicationService
    {
        Role GetById(Guid id);
        Role GetByName(string name);
        void Register(RegisterRoleCommand command);
        void Remove(RemoveRoleCommand command);
    }
}
