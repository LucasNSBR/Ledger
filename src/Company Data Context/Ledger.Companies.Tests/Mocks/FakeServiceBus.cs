using System.Threading.Tasks;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Commands;
using Ledger.Shared.IntegrationEvents.Events;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeServiceBus : IDomainServiceBus
    {
        public Task Publish(IntegrationEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task Send(Command command)
        {
            return Task.CompletedTask;
        }
    }
}
