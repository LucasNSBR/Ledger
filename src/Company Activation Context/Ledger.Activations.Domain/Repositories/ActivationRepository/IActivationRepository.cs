using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;
using System.Linq.Expressions;

namespace Ledger.Activations.Domain.Repositories.ActivationRepository
{
    public interface IActivationRepository
    {
        Activation GetById(Guid id);
        Activation GetByCompanyId(Guid id);
    }
}
