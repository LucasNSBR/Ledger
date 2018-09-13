using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Ledger.Activations.Tests.Mocks
{
    public class FakeServiceBus : IIntegrationServiceBus
    {
        public Task Publish<T>(T IntegrationEvent) where T : IntegrationEvent
        {
            return Task.CompletedTask;
        }
    }
}
