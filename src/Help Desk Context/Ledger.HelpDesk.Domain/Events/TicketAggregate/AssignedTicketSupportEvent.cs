using Ledger.Shared.Events;
using System;

namespace Ledger.HelpDesk.Domain.Events.TicketAggregate
{
    public class AssignedTicketSupportEvent : DomainEvent
    {
        public Guid TicketId { get; }
        public Guid UserId { get; }

        public AssignedTicketSupportEvent(Guid ticketId, Guid userId)
        {
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
