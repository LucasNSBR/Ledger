using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Activations.Tests.Aggregates
{
    [TestClass]
    public class ActivationTests
    {
        Company company;
        Activation activation;

        public ActivationTests()
        {
            company = new Company(Guid.NewGuid());
            activation = new Activation(company);
        }

        [TestMethod]
        public void CompanyIdAndActivationShouldBeSame()
        {
            Assert.AreEqual(company.Id, activation.Id);
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

        [TestMethod]
        public void ShouldAttachAndReturnDocuments()
        {
            byte[] owner = new byte[8];
            byte[] contratoSocial = new byte[8];
            byte[] alteracaoContratoSocial = new byte[8];

            activation.AttachCompanyDocuments(contratoSocial, alteracaoContratoSocial, owner);
            IReadOnlyList<byte[]> documents = activation.GetCompanyDocuments();

            Assert.IsNotNull(documents);
            Assert.AreEqual(3, documents.Count);
        }
    }
}
