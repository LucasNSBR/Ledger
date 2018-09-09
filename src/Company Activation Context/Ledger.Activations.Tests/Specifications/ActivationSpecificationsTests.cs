using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Specifications.ActivationSpecifications;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Activations.Tests.Specifications
{
    [TestClass]
    public class ActivationSpecificationsTests
    {
        [TestMethod]
        public void ShouldMatchUsingIdSpecification()
        {
            Guid companyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1");

            ActivationIdSpecification specification = new ActivationIdSpecification(companyId);
            
            Activation activation = new Activation(
                   new Company(companyId,
                   new Owner("Lucas Pereira Campos", DateTime.Now.AddYears(-25), new Cpf("981.153.856-99")))
                   );

            Assert.IsTrue(specification.IsSatisfiedBy(activation));
        }
    }
}
