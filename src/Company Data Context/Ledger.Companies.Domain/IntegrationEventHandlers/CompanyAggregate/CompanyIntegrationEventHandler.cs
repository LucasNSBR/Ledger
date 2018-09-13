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
        private readonly ICompanyRepository _repository;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;

        public CompanyIntegrationEventHandler(ICompanyRepository repository, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Consume(ConsumeContext<AcceptedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _repository.GetById(id);

            company.SetActive();

            _repository.Update(company);
            _unitOfWork.Commit();             

            //TODO: Add Email Service here

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RejectedCompanyActivationIntegrationEvent> context)
        {
            Guid id = context.Message.CompanyId;
            Company company = _repository.GetById(id);

            company.SetInactive();

            _repository.Update(company);
            _unitOfWork.Commit();

            //TODO: Add Email Service here

            return Task.CompletedTask;
        }
    }
}
