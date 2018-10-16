using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Commands;
using Ledger.Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.Blog.Tests.Mocks
{
    public class FakeDomainBus : IDomainServiceBus
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
