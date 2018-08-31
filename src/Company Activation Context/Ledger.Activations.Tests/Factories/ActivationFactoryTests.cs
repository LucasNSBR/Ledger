using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Shared.ValueObjects;
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

            Owner owner = new Owner("Lucas Pereira Campos", DateTime.Now.AddYears(-20), new Cpf("981.153.856-99"));
            Guid companyId = Guid.NewGuid();

            Activation activation = factory.CreateActivation(companyId, owner);

            Assert.IsNotNull(activation);
            Assert.AreEqual(companyId, activation.Id);
        }
    }
}
