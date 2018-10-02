using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Context;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Services.Dispatchers;
using Ledger.Shared.IntegrationEvents.Events.CompanyEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Activations.Domain.IntegrationEventHandlers.ActivationAggregate
{
    public class ActivationIntegrationEventHandler : IConsumer<RegisteredCompanyIntegrationEvent>
    {
        private readonly IActivationRepository _repository;
        private readonly IUnitOfWork<ILedgerActivationDbAbstraction> _unitOfWork;
        private readonly IActivationFactory _factory;
        private readonly IEmailDispatcher _emailDispatcher;

        public ActivationIntegrationEventHandler(IActivationRepository repository, IActivationFactory factory, IUnitOfWork<ILedgerActivationDbAbstraction> unitOfWork, IEmailDispatcher emailDispatcher)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _factory = factory;
            _emailDispatcher = emailDispatcher;
        }

        public async Task Consume(ConsumeContext<RegisteredCompanyIntegrationEvent> context)
        {
            Guid companyId = context.Message.CompanyId;
            string email = context.Message.Email;
            
            Activation activation = _factory.CreateActivation(companyId);

            _repository.Register(activation);
            _unitOfWork.Commit();

            EmailTemplate emailTemplate = new EmailTemplate(email)
                .SetTemplate(EmailTemplateTypes.CompanyRegistered);

            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }
    }
}
