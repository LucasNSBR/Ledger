using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Events.TicketAggregate;
using Ledger.HelpDesk.Domain.Factories;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.UserServices.IdentityResolver;
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
        private readonly ITicketFactory _factory;
        private readonly IIdentityResolver _identityResolver;

        public TicketApplicationService(ITicketRepository ticketRepository, ITicketCategoryRepository categoryRepository, ITicketFactory factory, IIdentityResolver identityResolver, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, domainBus)
        {
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
            _factory = factory;
            _identityResolver = identityResolver;
        }

        //Accessible only through a user with Support role on controller
        public IQueryable<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAllTickets();
        }

        public IQueryable<Ticket> GetByUserId(Guid userId)
        {
            LedgerIdentityUser user = _identityResolver.GetUser();
            if (userId != user.Id)
            {
                AddNotification("Erro ao buscar", "O usuário atual não possui permissão para visualizar os dados.");
                return Enumerable.Empty<Ticket>().AsQueryable();
            }

            return _ticketRepository.GetByUserId(userId);
        }

        public Ticket GetById(Guid id)
        {
            Ticket ticket = _ticketRepository.GetById(id);
            
            if (NotifyCantAccessTicket(ticket))
                return null;

            return ticket;
        }

        public void Register(RegisterTicketCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            TicketCategory category = _categoryRepository.GetById(command.CategoryId);
            LedgerIdentityUser user = _identityResolver.GetUser();

            if (NotifyNullCategory(category))
                return;

            Ticket ticket = _factory.OpenTicket(command.Title, command.Details, command.CategoryId, user.Id);

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
            LedgerIdentityUser user = _identityResolver.GetUser();

            if (NotifyNullTicket(ticket))
                return;

            if(ticket.TicketUserId != user.Id)
            {
                AddNotification("Erro ao anexar", "O usuário não tem permissão para anexar arquivos ao ticket.");
                return;
            }

            ticket.AttachIssuePicture(issuePicture);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            Commit();
        }

        //Accessible only through a user with Support role on controller
        public void AssignSupport(AssignSupportCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);
            LedgerIdentityUser user = _identityResolver.GetUser();
            
            if (NotifyNullTicket(ticket))
                return;

            ticket.AssignSupportUser(user.Id);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            if (Commit())
                PublishLocal(new AssignedTicketSupportEvent(ticket.Id, user.Id));
        }

        public void AddMessage(AddMessageCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);
            LedgerIdentityUser user = _identityResolver.GetUser();

            if (NotifyNullTicket(ticket))
                return;

            //User authenticity threated inside AddMessage() method
            ticket.AddMessage(command.Body, user.Id);

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

            if (Commit())
                PublishLocal(new AddedTicketMessageEvent(command.Body, ticket.Id, user.Id));
        }

        public void Close(CloseTicketCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Ticket ticket = _ticketRepository.GetById(command.TicketId);

            if (NotifyNullTicket(ticket) || NotifyCantAccessTicket(ticket))
                return;

            ticket.Close();

            if (AddNotifications(ticket))
                return;

            _ticketRepository.Update(ticket);

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

        private bool NotifyNullCategory(TicketCategory category)
        {
            if (category == null)
            {
                AddNotification("Id inválido", "A categoria de Ticket não pôde ser encontrada.");
                return true;
            }

            return false;
        }

        private bool NotifyCantAccessTicket(Ticket ticket)
        {
            LedgerIdentityUser user = _identityResolver.GetUser();
            LedgerIdentityRole supportRole = new LedgerIdentityRole(RoleTypes.Support);

            bool canAccessTicket = ticket.TicketUserId == user.Id || user.IsInRole(supportRole);

            if (!canAccessTicket)
            {
                AddNotification("Usuário inválido", "O usuário não tem autorização para acessar os dados.");
                return true;
            }

            return false;
        }
    }
}
