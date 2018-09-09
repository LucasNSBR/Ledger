using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using System;

namespace Ledger.Companies.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Company GetById(Guid id);
        Company GetByCnpj(string cnpj);
        void Register(Company company);
        void Update(Company company);
    }
}
