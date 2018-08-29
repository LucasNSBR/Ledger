using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ledger.Activations.Tests.Aggregates
{
    [TestClass]
    public class ActivationTests
    {
        Owner owner;
        Company company;
        Activation activation;

        public ActivationTests()
        {
            owner = new Owner("Lucas Pereira Campos", 20, new Cpf("981.153.856-99"), null);
            company = new Company(owner, null, null);
            activation = new Activation(company);
        }

        [TestMethod]
        public void StatusShouldBePending()
        {
            Assert.AreEqual(ActivationStatus.Pending, activation.Status);
        }

        [TestMethod]
        public void StatusShouldBeAccepted()
        {
            activation.SetAccepted();

            Assert.AreEqual(ActivationStatus.Accepted, activation.Status);
        }

        [TestMethod]
        public void StatusShouldBeRejected()
        {
            activation.SetRejected();

            Assert.AreEqual(ActivationStatus.Rejected, activation.Status);
        }

        [TestMethod]
        public void ShouldAddNotificationIfStatusIsRejected()
        {
            activation.SetRejected();
            activation.SetRejected();

            Assert.AreEqual("Erro de rejeição", activation.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldAddNotificationIfStatusIsAccepted()
        {
            activation.SetAccepted();
            activation.SetAccepted();

            Assert.AreEqual("Erro de aceitação", activation.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldResetStatusToPending()
        {
            activation.SetRejected();
            activation.ResetActivationProcess();

            Assert.AreEqual(ActivationStatus.Pending, activation.Status);
        }

        [TestMethod]
        public void ShouldAddNotificationIfNotReset()
        {
            activation.ResetActivationProcess();

            Assert.AreEqual("Erro de reinício", activation.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldResetStatusToAccepted()
        {
            activation.SetRejected();
            activation.ResetActivationProcess();

            Assert.AreEqual(ActivationStatus.Pending, activation.Status);
        }
    }
}
