using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.CommandHandlers;
using Ledger.Shared.Commands;
using Ledger.Shared.EventHandlers;
using Ledger.Shared.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus
{
    public class DomainServiceBus : IDomainServiceBus
    {
        private readonly IServiceProvider _provider;

        public DomainServiceBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Publish<TDomainEvent>(TDomainEvent @event, CancellationToken? cancellationToken = null) where TDomainEvent : DomainEvent
        {
            IEnumerable<IDomainEventHandler<TDomainEvent>> handlers = _provider.GetServices<IDomainEventHandler<TDomainEvent>>();
            
            if (handlers != null)
            {
                foreach (IDomainEventHandler<TDomainEvent> handler in handlers)
                {
                    handler.Handle(@event);
                }
            }

            return Task.CompletedTask;
        }

        public Task Send<TCommand>(TCommand command, CancellationToken? cancellationToken = null) where TCommand : Command
        {
            ICommandHandler<TCommand> handler = _provider.GetRequiredService<ICommandHandler<TCommand>>();

            if (handler != null)
                return handler.Handle(command);

            return Task.CompletedTask;
        }
    }
}
