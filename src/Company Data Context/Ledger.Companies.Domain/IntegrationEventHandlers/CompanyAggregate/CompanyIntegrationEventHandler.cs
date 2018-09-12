using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.Shared.IntegrationEvents.Events.ActivationEvents;
using Ledger.Shared.ValueObjects;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Companies.Domain.IntegrationEventHandlers.CompanyAggregate
{
    public class CompanyIntegrationEventHandler : IConsumer<AcceptedCompanyActivationIntegrationEvent>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;

        public CompanyIntegrationEventHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task Consume(ConsumeContext<AcceptedCompanyActivationIntegrationEvent> context)
        {
            EmailAddress email = new EmailAddress("ledger@gmail.com");
            Cnpj cnpj = new Cnpj(new Guid().ToString().Substring(0, 10));
            InscricaoEstadual inscricao = new InscricaoEstadual("51113");
            Address address = new Address(452, "One Way Microsoft", "Centro", "Perto do Prédio Grande", "Nova Serrana", "MG", "35519000");
            PhoneNumber phone = new PhoneNumber("555 418 9220");
            Company company = new Company("Ledger Activation", "No Description", email, cnpj, inscricao);

            _companyRepository.Register(company);

            return Task.CompletedTask;
        }
    }
}
