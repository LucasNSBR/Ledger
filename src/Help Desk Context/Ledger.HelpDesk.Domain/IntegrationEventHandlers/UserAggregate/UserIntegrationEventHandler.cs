using Ledger.Shared.IntegrationEvents.Events.UserEvents;
using MassTransit;
using System.Threading.Tasks;

namespace Ledger.HelpDesk.Domain.IntegrationEventHandlers.UserAggregate
{
    public class UserIntegrationEventHandler : IConsumer<UserRegisteredIntegrationEvent>,
                                               IConsumer<UserAddedSupportRoleIntegrationEvent>
    {
        public Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            throw new System.NotImplementedException();
        }

        public Task Consume(ConsumeContext<UserAddedSupportRoleIntegrationEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
