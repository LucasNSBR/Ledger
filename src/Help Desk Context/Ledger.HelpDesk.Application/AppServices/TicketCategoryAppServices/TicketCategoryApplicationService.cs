using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.Shared.Notifications;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices
{
    public class TicketCategoryApplicationService : BaseApplicationService, ITicketCategoryApplicationService
    {
        private readonly ITicketCategoryRepository _repository;

        public TicketCategoryApplicationService(ITicketCategoryRepository repository, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, domainBus)
        {
            _repository = repository;
        }

        public IQueryable<TicketCategory> GetAllCategories()
        {
            return _repository.GetAllCategories();
        }

        public TicketCategory GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Register(RegisterTicketCategoryCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            TicketCategory ticketCategory = new TicketCategory(command.Name);

            _repository.Register(ticketCategory);
        }

        public void Update(UpdateTicketCategoryCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            TicketCategory ticketCategory = new TicketCategory(command.Id, command.Name);

            _repository.Update(ticketCategory);
        }
    }
}
