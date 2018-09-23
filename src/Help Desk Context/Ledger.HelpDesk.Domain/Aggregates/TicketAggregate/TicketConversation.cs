using Ledger.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketConversation : Entity<TicketConversation>
    {
        private readonly List<TicketMessage> _messages;
        public IReadOnlyList<TicketMessage> Messages
        {
            get
            {
                return _messages;
            }
        }

        public void AddMessage(TicketMessage message)
        {
            _messages.Add(message);
        }

        public IReadOnlyList<TicketMessage> GetMessagesFrom(TicketUser user)
        {
            return _messages
                .Where(m => m.TicketUser == user)
                .ToList();
        }
    }
}
