using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class Ticket : Entity<Ticket>, IAggregateRoot
    {
        public TicketStatus TicketStatus { get; private set; }

        public Guid TicketCategoryId { get; private set; }
        public TicketCategory TicketCategory { get; private set; }

        public Guid ConversationId { get; private set; }
        public TicketConversation TicketConversation { get; private set; }

        public Guid TicketUserId { get; private set; }
        public TicketUser TicketUser { get; private set; }
        public Guid SupportUserId { get; private set; }
        public SupportUser SupportUser { get; private set; }

        public string Title { get; private set; }
        public string Details { get; private set; }
        public Image IssuePicture { get; private set; }

        public Ticket(string title, string details, TicketCategory category, TicketUser user)
        {
            Title = title;
            Details = details;
            TicketCategory = category;
            TicketUser = user;

            TicketStatus = new TicketStatus();
            TicketConversation = new TicketConversation();
        }

        public Ticket(Guid id, string title, string details, TicketCategory category, TicketUser user)
        {
            Id = id;
            Title = title;
            Details = details;
            TicketCategory = category;
            TicketUser = user;

            TicketStatus = new TicketStatus();
            TicketConversation = new TicketConversation();
        }

        public bool IsOpened()
        {
            return TicketStatus.Status == Status.Open;
        }

        public bool IsClosed()
        {
            return TicketStatus.Status == Status.Closed;
        }

        public void AttachIssuePicture(Image issuePicture)
        {
            IssuePicture = issuePicture;
        }

        public bool AlreadyHaveSupport()
        {
            return SupportUser != null;
        }

        public void AssignSupportUser(SupportUser user)
        {
            if (!AlreadyHaveSupport())
                SupportUser = user;
            else
                AddNotification("Suporte já definido", "Já existe um usuário de suporte resolvendo esse problema.");
        }

        public void AddSupportMessage(string body)
        {
            if (AlreadyHaveSupport())
                AddMessage(body, SupportUser);
            else
                AddNotification("Sem suporte", "Esse ticket ainda não tem nenhum usuário de suporte resolvendo o problema.");
        }

        public void AddUserMessage(string body)
        {
            AddMessage(body, TicketUser);
        }

        private void AddMessage(string body, User user)
        {
            TicketMessage message = new TicketMessage(body, user);

            TicketConversation.AddMessage(message);
        }

        public IReadOnlyList<TicketMessage> GetMessages()
        {
            return TicketConversation.Messages;
        }

        private IReadOnlyList<TicketMessage> GetMessagesFrom(User user)
        {
            return TicketConversation.GetMessagesFrom(user);
        }

        public IReadOnlyList<TicketMessage> GetSupportMessages()
        {
            return GetMessagesFrom(SupportUser);
        }

        public IReadOnlyList<TicketMessage> GetUserMessages()
        {
            return GetMessagesFrom(TicketUser);
        }

        public void Close()
        {
            TicketStatus.SetClosed();
        }
    }
}
