using Ledger.Shared.Commands;
using Ledger.Shared.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus.Abstractions
{
    public interface IServiceBus
    {
        Task Send(Command command);
        Task Publish(IntegrationEvent @event);
    }
}
