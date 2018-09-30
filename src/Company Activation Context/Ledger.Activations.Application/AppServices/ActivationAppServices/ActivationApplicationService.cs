using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using Ledger.Activations.Domain.Context;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Extensions;
using Ledger.Shared.IntegrationEvents.Events.ActivationEvents;
using Ledger.Shared.Notifications;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Activations.Application.AppServices.ActivationAppServices
{
    public class ActivationApplicationService : BaseApplicationService, IActivationApplicationService
    {
        private readonly IActivationRepository _repository;

        public ActivationApplicationService(IActivationRepository repository, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerActivationDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, integrationBus, domainBus)
        {
            _repository = repository;
        }

        public Activation GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void AttachCompanyDocuments(AttachCompanyDocumentsCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Image contratoSocialPicture = new Image(command.ContratoSocialPicture.ToBytes());
            Image alteracaoContratoSocialPicture = new Image(command.AlteracaoContratoSocialPicture.ToBytes());
            Image ownerDocumentPicture = new Image(command.OwnerDocumentPicture.ToBytes());
            Image extraDocumentPicture = new Image(command.ExtraDocument.ToBytes());

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNullActivation(activation))
                return;

            activation.AttachCompanyDocuments(contratoSocialPicture,
                alteracaoContratoSocialPicture,
                ownerDocumentPicture,
                extraDocumentPicture);

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            Commit();
        }

        public void Accept(AcceptActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNullActivation(activation))
                return;

            activation.SetAccepted();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            if (Commit())
                Publish(new AcceptedCompanyActivationIntegrationEvent(command.ActivationId, DateTime.Now));
        }

        public void Reject(RejectActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNullActivation(activation))
                return;

            activation.SetRejected();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            if (Commit())
                Publish(new RejectedCompanyActivationIntegrationEvent(command.ActivationId, DateTime.Now));
        }

        public void Reset(ResetActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNullActivation(activation))
                return;

            activation.ResetActivationProcess();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            Commit();
        }

        private bool NotifyNullActivation(Activation activation)
        {
            if (activation == null)
            {
                AddNotification("Id inválido", "A ativação não pôde ser encontrada.");
                return true;
            }

            return false;
        }
    }
}
