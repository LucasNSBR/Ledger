using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.CrossCutting.Data.Transactions;
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
        private readonly IActivationFactory _factory;

        public ActivationApplicationService(IActivationRepository repository, IActivationFactory factory, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork unitOfWork, IServiceBus serviceBus) : base(domainNotificationHandler, unitOfWork, serviceBus)
        {
            _repository = repository;
            _factory = factory;
        }

        public Activation GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        
        public void RegisterActivation(RegisterActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Owner owner = new Owner(command.OwnerName, command.OwnerBirthday, new Cpf(command.OwnerCpf));
            Activation activation = _factory.CreateActivation(command.CompanyId, owner);
            
            _repository.Register(activation);

            Commit();
        }

        public void AttachCompanyDocuments(AttachCompanyDocumentsCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNull(activation))
                return;

            activation.AttachCompanyDocuments(command.ContratoSocialPicture.ToBytes(), 
                command.AlteracaoContratoSocialPicture.ToBytes(), 
                command.OwnerDocumentPicture.ToBytes());

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            Commit();
        }

        public void AcceptActivation(AcceptActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNull(activation))
                return;

            activation.SetAccepted();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            if (Commit())
                _serviceBus.Publish(new AcceptedCompanyActivationIntegrationEvent(command.ActivationId, DateTime.Now));
        }


        public void RejectActivation(RejectActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNull(activation))
                return;

            activation.SetRejected();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            if (Commit())
                _serviceBus.Publish(new RejectedCompanyActivationIntegrationEvent(command.ActivationId, DateTime.Now));
        }

        public void ResetActivation(ResetActivationCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Activation activation = _repository.GetById(command.ActivationId);

            if (NotifyNull(activation))
                return;

            activation.ResetActivationProcess();

            if (AddNotifications(activation))
                return;

            _repository.Update(activation);

            if (Commit())
                _serviceBus.Publish(new ResetedCompanyActivationIntegrationEvent(command.ActivationId));
        }

        private bool NotifyNull(Activation activation)
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
