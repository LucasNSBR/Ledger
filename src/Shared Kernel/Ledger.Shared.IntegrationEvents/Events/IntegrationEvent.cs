using System;

namespace Ledger.Shared.IntegrationEvents.Events
{
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public Guid EventId { get; }
        public DateTime DateCreated { get; }
        public Guid CorrelationId { get; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }

        public IntegrationEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
