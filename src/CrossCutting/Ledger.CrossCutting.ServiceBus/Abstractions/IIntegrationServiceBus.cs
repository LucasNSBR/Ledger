using Ledger.Shared.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus.Abstractions
{
    //The "real" service bus implemented with MassTransit and RabbitMQ
    //This bus is responsable for Publish and Handling of IntegrationEvents
    public interface IIntegrationServiceBus
    {
        Task Publish<T>(T IntegrationEvent) where T: IntegrationEvent
    }
}
