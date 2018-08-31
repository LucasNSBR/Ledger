using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;

namespace Ledger.Activations.Domain.Repositories.ActivationRepository
{
    public interface IActivationRepository
    {
        Activation GetById(Guid id);
        void Register(Activation activation);
        void Update(Activation activation);
    }
}
