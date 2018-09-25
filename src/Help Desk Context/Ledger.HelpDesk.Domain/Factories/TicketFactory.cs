using System;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;

namespace Ledger.HelpDesk.Domain.Factories
{
    public class TicketFactory : ITicketFactory
    {
        public Ticket Open(Guid id, string title, string details, TicketCategory category, TicketUser user)
        {
            return new Ticket(id, title, details, category, user);
        }
    }
}
