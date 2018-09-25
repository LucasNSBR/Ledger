using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
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

        public SupportUser GetSupportUserById(Guid id)
        {
            return _dbSet
                .AsNoTracking()
                .OfType<SupportUser>()
                .FirstOrDefault(x => x.Id == id);
        }

        public TicketUser GetTicketUserById(Guid id)
        {
            return _dbSet
                .AsNoTracking()
                .OfType<TicketUser>()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Register(TicketUser user)
        {
            _dbContext.Add(user);
        }

        public void AddToSupport(TicketUser user)
        {
            _dbContext.Add(user);
        }
    }
}
