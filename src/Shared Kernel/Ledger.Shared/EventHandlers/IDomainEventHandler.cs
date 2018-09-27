using Ledger.Shared.Events;
using System.Threading.Tasks;

namespace Ledger.Shared.EventHandlers
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEvent
    {
        Task Handle(TDomainEvent @event);
    }
}
