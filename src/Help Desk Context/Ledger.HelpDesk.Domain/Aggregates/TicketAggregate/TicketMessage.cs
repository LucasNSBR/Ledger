using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketMessage : Entity<TicketMessage>
    {
        public Guid TicketUserId { get; private set; }
        public TicketUser TicketUser { get; private set; }
        public string Body { get; private set; }

        public TicketMessage(string body, TicketUser user)
        {
            Body = body;
        }
    }
}
