using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Context;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.Shared.IntegrationEvents.Events.ActivationEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Companies.Domain.IntegrationEventHandlers.CompanyAggregate
{
    public class CompanyIntegrationEventHandler : IConsumer<AcceptedCompanyActivationIntegrationEvent>,
                                                  IConsumer<RejectedCompanyActivationIntegrationEvent>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;

        public CompanyIntegrationEventHandler(ICompanyRepository companyRepository, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Consume(ConsumeContext<AcceptedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _companyRepository.GetById(id);

            company.SetActive();

            _companyRepository.Update(company);
            _unitOfWork.Commit();             

            //TODO: Add Email Service here

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RejectedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _companyRepository.GetById(id);

            company.SetInactive();

            _companyRepository.Update(company);
            _unitOfWork.Commit();

            //TODO: Add Email Service here

            return Task.CompletedTask;
        }
    }
}
