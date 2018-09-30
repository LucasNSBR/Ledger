using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Aggregates.Roles;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Events.TicketAggregate;
using Ledger.HelpDesk.Domain.Factories;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
using Ledger.Shared.Extensions;
using Ledger.Shared.Notifications;
using Ledger.Shared.ValueObjects;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Application.AppServices.TicketAppServices
{
    public class TicketApplicationService : BaseApplicationService, ITicketApplicationService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketCategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITicketFactory _factory;

        public TicketApplicationService(ITicketRepository ticketRepository, ITicketCategoryRepository categoryRepository, IUserRepository userRepository, IRoleRepository roleRepository, ITicketFactory factory, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, domainBus)
        {
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _factory = factory;
        }

        public IQueryable<Ticket> GetByUserId(Guid userId)
        {
            return _ticketRepository.GetByUserId(userId);
        }

        public Ticket GetById(Guid id)
        {
            return _ticketRepository.GetById(id);
        }

        public void Register(RegisterTicketCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            TicketCategory category = _categoryRepository.GetById(command.CategoryId);
            User user = _userRepository.GetById(command.UserId);

            if (NotifyNullCategory(category) || NotifyNullUser(user))
                return;

            Ticket ticket = _factory.OpenTicket(command.Title, command.Details, command.CategoryId, command.UserId);

            _ticketRepository.Register(ticket);

            if (Commit())
                PublishLocal(new TicketRegisteredEvent(ticket.Id, ticket.Title, ticket.Details));
        }

        public void AttachIssuePicture(AttachIssuePictureCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Image issuePicture = new Image(command.IssuePicture.ToBytes());

            Ticket ticket = _ticketRepository.GetById(command.TicketId);

            if (NotifyNullTicket(ticket))
                return;

            ticket.AttachIssuePicture(issuePicture);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            Commit();
        }

        public void AddMessage(AddMessageCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);
            User user = _userRepository.GetById(command.UserId);

            if (NotifyNullTicket(ticket) || NotifyNullUser(user))
                return;

            ticket.AddMessage(command.Body, user.Id);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            if (Commit())
                PublishLocal(new AddedTicketMessageEvent(command.Body, ticket.Id, user.Id));
        }

        public void AssignSupport(AssignSupportCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);
            User user = _userRepository.GetById(command.UserId);
            Role supportRole = _roleRepository.GetByName(RoleTypes.Support);

            if (supportRole == null)
                throw new InvalidOperationException("A role de suporte não existe.");

            if (NotifyNullTicket(ticket) || NotifyNullUser(user))
                return;

            if (!user.IsInRole(supportRole))
            {
                AddNotification("Não autorizado", "O usuário não pode ser indicado esse ticket porque não possui a permissão necessária.");
                return;
            }

            ticket.AssignSupportUser(user.Id);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            if (Commit())
                PublishLocal(new AssignedTicketSupportEvent(ticket.Id, user.Id));
        }

        public void Close(CloseTicketCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);

            if (NotifyNullTicket(ticket))
                return;

            ticket.Close();

            if (AddNotifications(ticket))
                return;

            if (Commit())
                PublishLocal(new TicketClosedEvent(ticket.Id));
        }

        private bool NotifyNullTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                AddNotification("Id inválido", "O ticket não pôde ser encontrado.");
                return true;
            }

            return false;
        }

        private bool NotifyNullUser(User user)
        {
            if (user == null)
            {
                AddNotification("Id inválido", "O usuário não pôde ser encontrado.");
                return true;
            }

            return false;
        }

        private bool NotifyNullCategory(TicketCategory category)
        {
            if (category == null)
            {
                AddNotification("Id inválido", "A categoria de Ticket não pôde ser encontrada.");
                return true;
            }

            return false;
        }
    }
}
