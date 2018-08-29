using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Specifications.ActivationSpecifications;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Activations.Tests.Specifications
{
    [TestClass]
    public class ActivationSpecifications
    {
        [TestMethod]
        public void ShouldMatchUsingCompanyIdSpecification()
        {
            ActivationCompanyIdSpecification specification = new ActivationCompanyIdSpecification(new Guid("9c0e0aa4-2618-4158-9714-dee8dd94b5ad"));

            Activation activation = new Activation(
                   new Company(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"),
                   new Owner("Lucas Pereira Campos", 20, new Cpf("981.153.856-99"), null), null, null)
                   );

            Assert.IsTrue(specification.IsSatisfiedBy(activation));
        }
    }
}
