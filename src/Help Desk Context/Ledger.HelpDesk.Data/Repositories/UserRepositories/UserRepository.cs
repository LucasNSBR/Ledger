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
        private readonly DbSet<SupportUser> _supportUsers;
        private readonly DbSet<TicketUser> _ticketUsers;

        public UserRepository(LedgerHelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
            _supportUsers = dbContext.SupportUsers;
            _ticketUsers = dbContext.TicketUsers;
        }

        public IQueryable<SupportUser> GetSupportUsers()
        {
            return _supportUsers
                .AsNoTracking();
        }

        public IQueryable<TicketUser> GetTicketUsers()
        {
            return _ticketUsers
                .AsNoTracking();
        }
        public SupportUser GetSupportUserById(Guid id)
        {
            return _supportUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public TicketUser GetTicketUserById(Guid id)
        {
            return _ticketUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Register(TicketUser user)
        {
            _dbContext.Add(user);
        }

        public void AddToSupport(SupportUser user)
        {
            _dbContext.Add(user);
        }
    }
}
