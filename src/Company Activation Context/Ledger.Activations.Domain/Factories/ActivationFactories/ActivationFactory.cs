using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using System;

namespace Ledger.Activations.Domain.Factories.ActivationFactories
{
    public class ActivationFactory : IActivationFactory
    {
        public Activation CreateActivation(Guid companyId)
        {
            if (companyId == null || companyId == Guid.Empty)
                throw new ArgumentException(nameof(companyId));
            
            Company company = new Company(companyId);
            Activation activation = new Activation(company);

            return activation;
        }
    }
}
