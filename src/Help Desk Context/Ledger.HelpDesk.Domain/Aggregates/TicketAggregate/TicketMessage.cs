using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketMessage : Entity<TicketMessage>
    {
        public Guid TicketUserId { get; private set; }
        public User TicketUser { get; private set; }
        public string Body { get; private set; }
        public DateTime MessageDate { get; private set; }

        protected TicketMessage() { }

        public TicketMessage(string body, User user)
        {
            Body = body;
            TicketUser = user;

            MessageDate = DateTime.Now;
        }
    }
}
