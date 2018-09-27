using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Factories
{
    public class TicketFactory : ITicketFactory
    {
        public Ticket OpenTicket(string title, string details, Guid categoryId, Guid userId)
        {
            Ticket ticket = new Ticket(title, details, categoryId, userId);
            return ticket;
        }
    }
}
