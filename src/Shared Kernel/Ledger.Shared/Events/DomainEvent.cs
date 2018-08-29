using System;

namespace Ledger.Shared.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; private set; }
        public DateTime DateCreated { get; private set; }

        public DomainEvent()
        {
            EventId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}
