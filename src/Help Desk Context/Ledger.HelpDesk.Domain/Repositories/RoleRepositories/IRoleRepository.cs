using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Repositories.RoleRepositories
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetAllRoles();
        Role GetById(Guid id);
        Role GetByName(string name);
        void Register(Role role);
        void Remove(Role role);
        bool RoleExists(string name);
    }
}
