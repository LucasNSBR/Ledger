using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
using Ledger.Shared.IntegrationEvents.Events.UserEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.HelpDesk.Domain.IntegrationEventHandlers.UserAggregate
{
    public class UserIntegrationEventHandler : IConsumer<UserRegisteredIntegrationEvent>,
                                               IConsumer<UserAddedSupportRoleIntegrationEvent>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork<ILedgerHelpDeskDbAbstraction> _unitOfWork;

        public UserIntegrationEventHandler(IUserRepository repository, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            string email = context.Message.Email;

            User user = new User(id, email);

            _repository.Register(user);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UserAddedSupportRoleIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            User user = _repository.GetById(id);

            if (user == null)
                throw new NotSupportedException("User not found");

            SupportRole supportRole = new SupportRole(user.Id);
            user.AddRole(supportRole);

            _repository.Update(user);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
