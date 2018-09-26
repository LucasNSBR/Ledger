using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.HelpDesk.Domain.Specifications.RoleSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Data.Repositories.RoleRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LedgerHelpDeskDbContext _dbContext;
        private readonly DbSet<Role> _dbSet;

        public RoleRepository(LedgerHelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Roles;
        }

        public IQueryable<Role> GetAllRoles()
        {
            return _dbSet
                .AsNoTracking();
        }

        public Role GetById(Guid id)
        {
            RoleIdSpecification specification = new RoleIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public Role GetByName(string name)
        {
            RoleNameSpecification specification = new RoleNameSpecification(name);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(Role role)
        {
            _dbContext.Add(role);
        }

        public void Remove(Role role)
        {
            _dbContext.Remove(role);
        }

        public bool RoleExists(string name)
        {
            Role existentRole = GetByName(name);

            if (existentRole == null)
                return false;

            return true;
        }
    }
}
