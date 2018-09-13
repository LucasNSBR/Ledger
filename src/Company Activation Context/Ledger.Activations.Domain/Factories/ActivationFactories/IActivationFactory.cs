using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;

namespace Ledger.Activations.Domain.Factories.ActivationFactories
{
    public interface IActivationFactory
    {
        Activation CreateActivation(Guid companyId);
    }
}
