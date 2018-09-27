using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Context;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Services.Dispatchers;
using Ledger.CrossCutting.EmailService.Services.Factories;
using Ledger.Shared.IntegrationEvents.Events.ActivationEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Companies.Domain.IntegrationEventHandlers.CompanyAggregate
{
    public class CompanyIntegrationEventHandler : IConsumer<AcceptedCompanyActivationIntegrationEvent>,
                                                  IConsumer<RejectedCompanyActivationIntegrationEvent>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;
        private readonly IEmailFactory _emailFactory;
        private readonly IEmailDispatcher _emailDispatcher;

        public CompanyIntegrationEventHandler(ICompanyRepository repository, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork, IEmailFactory emailFactory, IEmailDispatcher emailDispatcher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _emailFactory = emailFactory;
            _emailDispatcher = emailDispatcher;
        }

        public async Task Consume(ConsumeContext<AcceptedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _repository.GetById(id);

            if (company == null)
                throw new InvalidOperationException("Company not found");

            company.SetActive();

            _repository.Update(company);
            _unitOfWork.Commit();

            EmailTemplate emailTemplate = _emailFactory.CreateCompanyActivationAcceptedEmail(company.Email.Email);
            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }

        public async Task Consume(ConsumeContext<RejectedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _repository.GetById(id);

            if (company == null)
                throw new InvalidOperationException("Company not found");

            company.SetInactive();

            _repository.Update(company);
            _unitOfWork.Commit();

            EmailTemplate emailTemplate = _emailFactory.CreateCompanyActivationRejectedEmail(company.Email.Email);
            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }
    }
}
