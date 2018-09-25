using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        IQueryable<SupportUser> GetSupportUsers();
        IQueryable<TicketUser> GetTicketUsers();
        SupportUser GetSupportUserById(Guid id);
        TicketUser GetTicketUserById(Guid id);
        void Register(TicketUser user);
        void AddToSupport(SupportUser user);
    }
}
