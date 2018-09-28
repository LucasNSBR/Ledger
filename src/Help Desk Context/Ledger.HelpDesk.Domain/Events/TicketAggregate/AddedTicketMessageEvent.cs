using Ledger.Shared.Events;
using System;

namespace Ledger.HelpDesk.Domain.Events.TicketAggregate
{
    public class AddedTicketMessageEvent : DomainEvent
    {
        public string Body { get; }
        public Guid TicketId { get; }
        public Guid UserId { get; }

        public AddedTicketMessageEvent(string body, Guid ticketId, Guid userId)
        {
            Body = body;
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
