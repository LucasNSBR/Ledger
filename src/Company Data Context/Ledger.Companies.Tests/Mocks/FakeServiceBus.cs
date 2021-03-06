﻿using System.Threading;
using System.Threading.Tasks;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Commands;
using Ledger.Shared.Events;
using Ledger.Shared.IntegrationEvents.Events;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeServiceBus : IDomainServiceBus, IIntegrationServiceBus
    {
        public Task Publish<TDomainEvent>(TDomainEvent @event, CancellationToken? cancellationToken = null) where TDomainEvent : DomainEvent
        {
            return Task.CompletedTask;
        }

        public Task Publish<T>(T IntegrationEvent) where T : IntegrationEvent
        {
            return Task.CompletedTask;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken? cancellationToken = null) where TCommand : Command
        {
            return Task.CompletedTask;
        }
    }
}
