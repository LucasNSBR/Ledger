using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles;
using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class Ticket : Entity<Ticket>, IAggregateRoot
    {
        public TicketStatus TicketStatus { get; private set; }

        public Guid CategoryId { get; private set; }

        public Guid ConversationId { get; private set; }
        public TicketConversation Conversation { get; private set; }

        public Guid TicketUserId { get; private set; }
        public Guid SupportUserId { get; private set; }

        public string Title { get; private set; }
        public string Details { get; private set; }
        public Image IssuePicture { get; private set; }

        protected Ticket() { }

        public Ticket(string title, string details, Guid categoryId, Guid userId)
        {
            Title = title;
            Details = details;
            CategoryId = categoryId;
            TicketUserId = userId;

            TicketStatus = new TicketStatus();
            Conversation = new TicketConversation();
        }

        public Ticket(Guid id, string title, string details, Guid categoryId, Guid userId)
        {
            Id = id;
            Title = title;
            Details = details;
            CategoryId = categoryId;
            TicketUserId = userId;

            TicketStatus = new TicketStatus();
            Conversation = new TicketConversation();
        }

        public bool IsOpened()
        {
            return TicketStatus.Status == Status.Open;
        }

        public void AttachIssuePicture(Image issuePicture)
        {
            IssuePicture = issuePicture;
        }

        public bool AlreadyHaveSupport()
        {
            return SupportUserId != null;
        }

        public void AssignSupportUser(User user)
        {
            if (!AlreadyHaveSupport())
            {
                if (user.IsInRole(RoleTypes.Support))
                    SupportUserId = user.Id;
                else
                    AddNotification("Usuário proibido", "O usuário especificado não possui as permissões necessárias para prestar suporte.");
            }
            else
                AddNotification("Suporte já definido", "Já existe um usuário de suporte resolvendo esse problema.");
        }

        public void AddMessage(string body, Guid userId)
        {
            if (!IsOpened())
            {
                AddNotification("Ticket finalizado", "Não é possível modificar um ticket que já foi resolvido.");
                return;
            }

            if (userId == SupportUserId || userId == TicketUserId)
            {
                TicketMessage message = new TicketMessage(body, userId);
                Conversation.AddMessage(message);
            }
            else
                AddNotification("Não possui acesso às mensagens", "O usuário não tem permissão para participar dessa conversa.");
        }

        public IReadOnlyList<TicketMessage> GetMessages()
        {
            return Conversation.GetMessages();
        }

        public void Close()
        {
            if (!IsOpened())
                AddNotification("Ticket finalizado", "Não é possível finalizar um ticket que já foi finalizado.");
            else
                TicketStatus.SetClosed();
        }
    }
}
