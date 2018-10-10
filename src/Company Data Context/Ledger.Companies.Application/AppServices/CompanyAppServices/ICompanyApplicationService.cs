using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using System;
using System.Linq;

namespace Ledger.Companies.Application.AppServices.CompanyAppServices
{
    public interface ICompanyApplicationService
    {
        IQueryable<Company> GetAllCompanies();
        Company GetById(Guid id);
        void Register(RegisterCompanyCommand command);
        void Update(UpdateCompanyCommand command);
        void ChangeAddress(ChangeCompanyAddressCommand command);
        void ChangePhone(ChangeCompanyPhoneCommand command);
    }
}
