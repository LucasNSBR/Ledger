using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Companies.Domain.Factories.CompanyFactories
{
    public class CompanyFactory : ICompanyFactory
    {
        public Company CreateCompany(string companyName, string companyDescription, string companyEmail, string companyCnpj, string companyInscricaoEstadual, string ownerName, DateTime ownerBirthday, string ownerCpf, Guid? companyId = null)
        {
            //Build company related VO's
            EmailAddress email = new EmailAddress(companyEmail);
            Cnpj cnpj = new Cnpj(companyCnpj);
            InscricaoEstadual inscricaoEstadual = new InscricaoEstadual(companyInscricaoEstadual);

            //Build owner related VO's
            Cpf cpf = new Cpf(ownerCpf);
            Owner owner = new Owner(ownerName, ownerBirthday, cpf);

            //Use the parameter id or create a new one
            Guid id = companyId ?? Guid.NewGuid();

            //Build company
            Company company = new Company(id, companyName, companyDescription, email, cnpj, inscricaoEstadual, owner);

            return company;
        }
    }
}
