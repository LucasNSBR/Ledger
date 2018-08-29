using System.Collections.Generic;

namespace Ledger.Shared.Events
{
    public interface IEventProducer
    {
        IReadOnlyList<DomainEvent> GetEvents();
        void AddEvent(DomainEvent @event);
        bool HasEvents();
    }
}
