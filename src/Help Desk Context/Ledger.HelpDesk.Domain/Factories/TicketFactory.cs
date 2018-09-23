using System;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;

namespace Ledger.HelpDesk.Domain.Factories
{
    public class TicketFactory : ITicketFactory
    {
        public Ticket Open(Guid id, string title, string details, TicketCategory category)
        {
            return new Ticket(id, title, details, category);
        }
    }
}
