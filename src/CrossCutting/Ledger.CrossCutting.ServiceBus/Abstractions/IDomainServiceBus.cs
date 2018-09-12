using Ledger.Shared.Commands;
using Ledger.Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus.Abstractions
{
    //Simplified Service Bus implementation to handle Domain Events
    //To handle IntegrationEvents you'll need to use MassTransit Bus
    public interface IDomainServiceBus
    {
        Task Send<TCommand>(TCommand command, CancellationToken? cancellationToken = null) where TCommand : Command;
        Task Publish<TDomainEvent>(TDomainEvent @event, CancellationToken? cancellationToken = null) where TDomainEvent : DomainEvent;
    }
}
