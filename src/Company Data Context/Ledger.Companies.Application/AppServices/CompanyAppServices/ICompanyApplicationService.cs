using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using System;

namespace Ledger.Companies.Application.AppServices.CompanyAppServices
{
    public interface ICompanyApplicationService
    {
        Company GetById(Guid id);
        Company GetByCnpj(string cnpj);
        void Register(RegisterCompanyCommand command);
        void Update(UpdateCompanyCommand command);
        void ChangeAddress(ChangeCompanyAddressCommand command);
        void ChangePhone(ChangeCompanyPhoneCommand command);
    }
}
