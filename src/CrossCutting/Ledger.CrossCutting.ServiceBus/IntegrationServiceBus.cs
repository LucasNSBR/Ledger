using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.IntegrationEvents.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus
{
    public class IntegrationServiceBus : IIntegrationServiceBus
    {
        private readonly IBusControl _bus;

        public IntegrationServiceBus(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task Publish<TIntegrationEvent>(TIntegrationEvent IntegrationEvent) where TIntegrationEvent : IntegrationEvent
        {
            await _bus.Publish(IntegrationEvent);
        }
    }
}
