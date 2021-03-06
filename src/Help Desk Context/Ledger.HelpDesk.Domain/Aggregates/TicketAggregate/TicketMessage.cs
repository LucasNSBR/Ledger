﻿using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketMessage : Entity<TicketMessage>
    {
        public Guid ConversationId { get; private set; }
        public Guid UserId { get; private set; }
        public string Body { get; private set; }
        public DateTime MessageDate { get; private set; }
        
        protected TicketMessage() { }

        public TicketMessage(string body, Guid userId, Guid conversationId)
        {
            Body = body;
            UserId = userId;
            ConversationId = conversationId;

            MessageDate = DateTime.Now;
        }
    }
}
