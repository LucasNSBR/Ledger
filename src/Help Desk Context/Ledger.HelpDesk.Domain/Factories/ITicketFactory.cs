using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Factories
{
    public interface ITicketFactory
    {
        Ticket OpenTicket(string title, string details, Guid categoryId, Guid userId);
    }
}
