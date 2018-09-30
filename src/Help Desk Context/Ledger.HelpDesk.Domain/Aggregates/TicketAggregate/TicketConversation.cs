using Ledger.Shared.Entities;
using System;
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

        public TicketConversation()
        {
            _messages = new List<TicketMessage>();
        }

        public IReadOnlyList<TicketMessage> GetMessagesByDate()
        {
            return _messages
                .OrderBy(c => c.MessageDate)
                .ToList();
        }

        public void AddMessage(string body, Guid userId)
        {
            TicketMessage message = new TicketMessage(body, userId, Id);
            _messages.Add(message);
        }
    }
}
