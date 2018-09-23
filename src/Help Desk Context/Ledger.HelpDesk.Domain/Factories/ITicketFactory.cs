using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Factories
{
    public interface ITicketFactory
    {
        Ticket Open(Guid id, string title, string details, TicketCategory category, TicketUser user);
    }
}
