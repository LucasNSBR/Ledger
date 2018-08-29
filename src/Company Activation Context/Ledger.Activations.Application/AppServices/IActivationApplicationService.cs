using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;

namespace Ledger.Activations.Application.AppServices
{
    public interface IActivationApplicationService
    {
        Activation GetById(Guid id);
        Activation GetByCompanyId(Guid id);
    }
}
