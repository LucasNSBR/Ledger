using System.Threading;
using System.Threading.Tasks;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Commands;
using Ledger.Shared.Events;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeServiceBus : IDomainServiceBus
    {
        public Task Publish<TDomainEvent>(TDomainEvent @event, CancellationToken? cancellationToken = null) where TDomainEvent : DomainEvent
        {
            return Task.CompletedTask;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken? cancellationToken = null) where TCommand : Command
        {
            return Task.CompletedTask;
        }
    }
}
