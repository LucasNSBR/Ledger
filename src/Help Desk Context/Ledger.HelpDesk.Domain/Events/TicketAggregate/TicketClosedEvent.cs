using Ledger.Shared.Events;
using System;

namespace Ledger.HelpDesk.Domain.Events.TicketAggregate
{
    public class TicketClosedEvent : DomainEvent
    {
        public Guid TicketId { get; }

        public TicketClosedEvent(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}
