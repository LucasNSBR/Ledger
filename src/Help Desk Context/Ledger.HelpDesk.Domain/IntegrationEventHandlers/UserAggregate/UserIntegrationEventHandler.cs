using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
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

        public UserIntegrationEventHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            string email = context.Message.Email;

            TicketUser user = new TicketUser(id, email);

            _repository.Register(user);

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UserAddedSupportRoleIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            string email = context.Message.Email;

            SupportUser user = new SupportUser(id, email);

            _repository.AddToSupport(user);

            return Task.CompletedTask;
        }
    }
}
