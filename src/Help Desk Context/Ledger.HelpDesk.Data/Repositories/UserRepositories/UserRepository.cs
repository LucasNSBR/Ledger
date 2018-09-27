using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
using Ledger.HelpDesk.Domain.Specifications.UserSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Data.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LedgerHelpDeskDbContext _dbContext;
        private readonly DbSet<User> _dbSet;

        public UserRepository(LedgerHelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Users;
        }
        
        public User GetById(Guid id)
        {
            UserIdSpecification specification = new UserIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .Include(r => r.Roles)
                .FirstOrDefault(specification.ToExpression());
        }
        
        public void Register(User user)
        {
            _dbContext.Add(user);
        }

        public void Update(User user)
        {
            _dbContext.Update(user);
        }
    }
}
