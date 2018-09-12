using System;

namespace Ledger.Shared.IntegrationEvents.Events
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; }
        DateTime DateCreated { get; }
        Guid CorrelationId { get; }
    }
}
