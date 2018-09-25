using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        SupportUser GetSupportUserById(Guid id);
        TicketUser GetTicketUserById(Guid id);
        void Register(TicketUser user);
        void AddToSupport(SupportUser user);
    }
}
