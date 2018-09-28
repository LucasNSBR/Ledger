using Ledger.Shared.Events;
using System;

namespace Ledger.HelpDesk.Domain.Events.TicketAggregate
{
    public class TicketRegisteredEvent : DomainEvent
    {
        public Guid TicketId { get; }
        public string Title { get; }
        public string Details { get; }

        public TicketRegisteredEvent(Guid ticketId, string title, string details)
        {
            TicketId = ticketId;
            Title = title;
            Details = details;
        }
    }
}
