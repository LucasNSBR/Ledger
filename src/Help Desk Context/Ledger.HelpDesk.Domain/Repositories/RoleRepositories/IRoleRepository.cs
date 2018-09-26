using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Repositories.RoleRepositories
{
    public interface IRoleRepository
    {
        Role GetById(Guid id);
        Role GetByName(string name);
        void Register(Role role);
        void Remove(Role role);
        bool RoleExists(string name);
    }
}
