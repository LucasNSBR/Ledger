using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Companies.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Company GetById(Guid id);
        Company GetByCnpj(string cnpj);
        bool CnpjExists(Cnpj cnpj, Guid id);
        void Register(Company company);
        void Update(Company company);
    }
}
