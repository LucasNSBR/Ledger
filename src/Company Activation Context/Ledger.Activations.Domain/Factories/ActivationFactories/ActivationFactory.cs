using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;

namespace Ledger.Activations.Domain.Factories.ActivationFactories
{
    public class ActivationFactory : IActivationFactory
    {
        public Activation CreateActivation(Guid companyId, Owner owner)
        {
            if (companyId == null || companyId == Guid.Empty)
                throw new ArgumentException(nameof(companyId));
            if (owner == null)
                throw new ArgumentException(nameof(owner));

            Company company = new Company(companyId, owner);
            Activation activation = new Activation(company);

            return activation;
        }
    }
}
