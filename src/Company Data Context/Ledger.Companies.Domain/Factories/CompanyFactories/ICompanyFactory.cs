using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using System;

namespace Ledger.Companies.Domain.Factories.CompanyFactories
{
    public interface ICompanyFactory
    {
        Company CreateCompany(string name, string description, string email, string cnpj, string inscricaoEstadual, string ownerName, DateTime ownerBirthday, string ownerCpf, Guid? companyId = null);
    }
}
