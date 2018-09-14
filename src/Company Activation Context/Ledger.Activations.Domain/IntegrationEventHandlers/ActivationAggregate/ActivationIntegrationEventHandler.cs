using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Context;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Services.Dispatchers;
using Ledger.CrossCutting.EmailService.Services.Factories;
using Ledger.Shared.IntegrationEvents.Events.CompanyEvents;
using Ledger.Shared.Notifications;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Activations.Domain.IntegrationEventHandlers.ActivationAggregate
{
    public class ActivationIntegrationEventHandler : IConsumer<RegisteredCompanyIntegrationEvent>
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IActivationRepository _repository;
        private readonly IUnitOfWork<ILedgerActivationDbAbstraction> _unitOfWork;
        private readonly IActivationFactory _factory;
        private readonly IEmailFactory _emailFactory;
        private readonly IEmailDispatcher _emailDispatcher;

        public ActivationIntegrationEventHandler(DomainNotificationHandler domainNotificationHandler, IActivationRepository repository, IActivationFactory factory, IUnitOfWork<ILedgerActivationDbAbstraction> unitOfWork, IEmailFactory emailFactory, IEmailDispatcher emailDispatcher)
        {
            _unitOfWork = unitOfWork;
            _domainNotificationHandler = domainNotificationHandler;
            _repository = repository;
            _factory = factory;
            _emailFactory = emailFactory;
            _emailDispatcher = emailDispatcher;
        }

        public async Task Consume(ConsumeContext<RegisteredCompanyIntegrationEvent> context)
        {
            Guid companyId = context.Message.CompanyId;
            string email = context.Message.Email;
            
            Activation activation = _factory.CreateActivation(companyId);

            _repository.Register(activation);
            _unitOfWork.Commit();

            EmailTemplate emailTemplate = _emailFactory.CreateCompanyRegisteredEmail(email);
            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }
    }
}
