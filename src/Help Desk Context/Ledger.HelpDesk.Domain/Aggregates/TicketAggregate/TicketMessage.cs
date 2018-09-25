using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketMessage : Entity<TicketMessage>
    {
        public Guid UserId { get; private set; }
        public string Body { get; private set; }
        public DateTime MessageDate { get; }

        protected TicketMessage() { }

        public TicketMessage(string body, Guid userId)
        {
            Body = body;
            UserId = userId;

            MessageDate = DateTime.Now;
        }
    }
}
