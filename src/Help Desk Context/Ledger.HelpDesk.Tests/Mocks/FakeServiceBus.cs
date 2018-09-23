using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Commands;
using Ledger.Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.HelpDesk.Tests.Mocks
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
