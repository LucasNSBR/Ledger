using Ledger.Activations.Application.AppServices.ActivationAppServices;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Tests.Mocks;
using Ledger.Shared.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Activations.Tests.AppServices
{
    [TestClass]
    public class ActivationApplicationServiceTests
    {
        Guid companyId = new Guid("399b3510-3c3e-4d0a-85a0-a4723d492883");

        FakeActivationRepository activationRepository;
        ActivationFactory activationFactory;
        DomainNotificationHandler domainNotificationHandler;
        FakeUnitOfWork fakeUnitOfWork;
        FakeServiceBus serviceBus;
        FakeDomainServiceBus domainBus;
        ActivationApplicationService service;

        public ActivationApplicationServiceTests()
        {
            activationFactory = new ActivationFactory();
            domainNotificationHandler = new DomainNotificationHandler();
            fakeUnitOfWork = new FakeUnitOfWork();
            serviceBus = new FakeServiceBus();
            domainBus = new FakeDomainServiceBus();
            activationRepository = new FakeActivationRepository();

            service = new ActivationApplicationService(activationRepository, activationFactory, domainNotificationHandler, fakeUnitOfWork, serviceBus, domainBus);
        }

        //Helpers
        private void PopulateRepository()
        {
            activationRepository.Register(new Activation
                (new Company(companyId)));   
        }

        private Activation GetActivation()
        {
            return service.GetById(companyId);
        }

        [TestMethod]
        public void ShouldRegisterAnActivation()
        {
            PopulateRepository();

            Assert.IsNotNull(activationRepository.GetById(companyId));
        }

        [TestMethod]
        public void ShouldAcceptActivation()
        {
            PopulateRepository();

            AcceptActivationCommand command = new AcceptActivationCommand
            {
                ActivationId = companyId
            };

            service.AcceptActivation(command);

            Activation activation = GetActivation();

            Assert.AreEqual(ActivationStatus.Accepted, activation.Status);
        }

        [TestMethod]
        public void SnouldRejectActivation()
        {
            PopulateRepository();

            RejectActivationCommand command = new RejectActivationCommand
            {
                ActivationId = companyId
            };

            service.RejectActivation(command);

            Activation activation = GetActivation();

            Assert.AreEqual(ActivationStatus.Rejected, activation.Status);
        }

        [TestMethod]
        public void ShouldResetActivationToPendingStatus()
        {
            PopulateRepository();

            RejectActivationCommand command = new RejectActivationCommand
            {
                ActivationId = companyId
            };

            service.RejectActivation(command);

            Activation activation = GetActivation();

            Assert.AreEqual(ActivationStatus.Rejected, activation.Status);

            ResetActivationCommand resetCommand = new ResetActivationCommand
            {
                ActivationId = companyId
            };

            service.ResetActivation(resetCommand);

            Assert.AreEqual(ActivationStatus.Pending, activation.Status);
        }

        [TestMethod]
        public void ShouldAttachDocumentsAndReturn()
        {
            PopulateRepository();

            string owner = "bHVjYXM=";
            string extra = "bHVjYXM=";
            string contratoSocial = "bHVjYXM=";
            string alteracaoContratoSocial = "bHVjYXM=";

            AttachCompanyDocumentsCommand command = new AttachCompanyDocumentsCommand
            {
                ActivationId = companyId,
                OwnerDocumentPicture = owner,
                ContratoSocialPicture = contratoSocial,
                AlteracaoContratoSocialPicture = alteracaoContratoSocial,
                ExtraDocument = extra 
            };

            service.AttachCompanyDocuments(command);

            Activation activation = GetActivation();

            Assert.IsNotNull(activation.GetCompanyDocuments());
            Assert.AreEqual(4, activation.GetCompanyDocuments().Count);
        }
    }
}
