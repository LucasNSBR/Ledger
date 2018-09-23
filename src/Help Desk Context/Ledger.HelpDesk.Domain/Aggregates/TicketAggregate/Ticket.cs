using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class Ticket : Entity<Ticket>, IAggregateRoot
    {
        public TicketStatus Status { get; private set; }
        public Guid TicketCategoryId { get; private set; }
        public TicketCategory TicketCategory { get; private set; }
        public Guid ConversationId { get; private set; }
        public TicketConversation TicketConversation { get; private set; }
        public string Title { get; private set; }
        public string Details { get; private set; }
        public Image IssuePicture { get; private set; }

        public Ticket(string title, string details, TicketCategory category)
        {
            Open();

            Title = title;
            Details = details;
            TicketCategory = category;

            TicketConversation = new TicketConversation();
        }

        public Ticket(Guid id, string title, string details, TicketCategory category)
        {
            Open();

            Id = id;
            Title = title;
            Details = details;
            TicketCategory = category;

            TicketConversation = new TicketConversation();
        }

        public void AttachIssuePicture(Image issuePicture)
        {
            IssuePicture = issuePicture;
        }

        private void Open()
        {
            Status = TicketStatus.Open;
        }

        public IReadOnlyList<TicketMessage> GetMessages()
        {
            return TicketConversation.Messages;
        } 

        public void AddMessage(TicketMessage message)
        {
            TicketConversation.AddMessage(message);
        }

        public IReadOnlyList<TicketMessage> GetMessagesFrom(TicketUser user)
        {
            return TicketConversation.GetMessagesFrom(user);
        } 

        public void Close()
        {
            Status = TicketStatus.Open;
        }
    }
}
