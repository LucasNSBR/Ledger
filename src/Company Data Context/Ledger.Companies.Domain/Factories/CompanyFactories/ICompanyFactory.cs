using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using System;

namespace Ledger.Companies.Domain.Factories.CompanyFactories
{
    public interface ICompanyFactory
    {
        Company CreateCompany(string companyName, string companyDescription, string companyEmail, string companyCnpj, string companyInscricaoEstadual, string ownerName, DateTime ownerBirthday, string ownerCpf, Guid tenantId, Guid? companyId = null);
    }
}
