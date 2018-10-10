using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Activations.Tests.Factories
{
    [TestClass]
    public class ActivationFactoryTests
    {
        [TestMethod]
        public void ShouldReturnAnActivation()
        {
            IActivationFactory factory = new ActivationFactory();

            Guid companyId = Guid.NewGuid();
            Guid tenantId = Guid.NewGuid();

            Activation activation = factory.CreateActivation(companyId, tenantId);

            Assert.IsNotNull(activation);
            Assert.AreEqual(companyId, activation.Id);
        }
    }
}
