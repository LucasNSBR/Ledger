using System;

namespace Ledger.Shared.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; }
        public DateTime DateCreated { get; }

        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}
