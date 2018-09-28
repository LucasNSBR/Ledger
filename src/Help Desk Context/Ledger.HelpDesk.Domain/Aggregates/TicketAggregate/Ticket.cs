using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Specifications.TicketSpecifications.TicketMessageSpecifications;
using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class Ticket : Entity<Ticket>, IAggregateRoot
    {
        public TicketStatus TicketStatus { get; private set; }

        public Guid CategoryId { get; private set; }

        public Guid ConversationId { get; private set; }
        public TicketConversation Conversation { get; private set; }

        public Guid TicketUserId { get; private set; }
        public Guid? SupportUserId { get; private set; }

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

        private bool ContainsPicture()
        {
            return IssuePicture != null;
        }

        public void AttachIssuePicture(Image issuePicture)
        {
            if (!ContainsPicture())
                IssuePicture = issuePicture;
            else
                AddNotification("Imagem já existente", "Uma imagem do erro já está anexada ao ticket de suporte.");
        }

        public bool AlreadyHaveSupport()
        {
            return SupportUserId != null;
        }

        public void AssignSupportUser(User user)
        {
            if (!AlreadyHaveSupport())
                SupportUserId = user.Id;
            else
                AddNotification("Suporte já definido", "Já existe um usuário de suporte resolvendo esse problema.");
        }

        public void AddMessage(string body, Guid userId)
        {
            if (NotifyClosedTicket())
                return;

            if (userId == SupportUserId || userId == TicketUserId)
            {
                TicketMessage message = new TicketMessage(body, userId);
                Conversation.AddMessage(message);
            }
            else
                AddNotification("Não possui acesso às mensagens", "O usuário não tem permissão para participar dessa conversa.");
        }

        public IReadOnlyList<TicketMessage> GetMessages(Guid? userId = null)
        {
            IReadOnlyList<TicketMessage> messages = Conversation.GetMessages();

            if (userId != null)
            {
                TicketMessageUserIdSpecification specification = new TicketMessageUserIdSpecification(userId.Value);

                return messages
                    .Where(specification.Compile())
                    .ToList();
            }

            return messages;
        }

        public void Close()
        {
            if (NotifyClosedTicket())
                return;

            TicketStatus.SetClosed();
        }

        private bool NotifyClosedTicket()
        {
            if (!IsOpened())
            {
                AddNotification("Ticket finalizado", "Não é possível modificar um ticket que já foi resolvido.");
                return true;
            }

            return false;
        }
    }
}
