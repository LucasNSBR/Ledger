using Ledger.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketConversation : Entity<TicketConversation>
    {
        private readonly List<TicketMessage> _messages;

        public TicketConversation()
        {
            _messages = new List<TicketMessage>();
        }

        public IReadOnlyList<TicketMessage> GetMessages()
        {
            return _messages
                .OrderBy(c => c.MessageDate)
                .ToList();
        }

        public void AddMessage(TicketMessage message)
        {
            _messages.Add(message);
        }
    }
}
