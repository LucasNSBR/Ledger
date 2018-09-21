using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Repositories;
using Ledger.Companies.Domain.Specifications.CompanySpecifications;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeCompanyRepository : ICompanyRepository
    {
        private List<Company> _companies;

        public FakeCompanyRepository()
        {
            _companies = new List<Company>();

            Populate();
        }

        private void Populate()
        {
            Guid companyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1");
            _companies.Add(new Company(companyId,
               "Fabrikam", "Fabrikam Corporation", new EmailAddress("admin@fabrikam.com"), new Cnpj("59009518000141"), new InscricaoEstadual("001.115 -550"),
                           new Owner("Lucas Pereira Campos", DateTime.Now.AddYears(-20), new Cpf("981.153.856-99")))
            );
        }

        public List<Company> GetCompanies()
        {
            return _companies;
        }

        public Company GetById(Guid id)
        {
            CompanyIdSpecification spec = new CompanyIdSpecification(id);

            return _companies.AsQueryable().FirstOrDefault(spec.ToExpression());
        }

        public Company GetByCnpj(string cnpj)
        {
            CompanyCnpjSpecification spec = new CompanyCnpjSpecification(cnpj);

            return _companies.AsQueryable().FirstOrDefault(spec.ToExpression());
        }

        public void Register(Company company)
        {
            _companies.Add(company);
        }

        public void Update(Company company)
        {
            int index = _companies.FindIndex(a => a.Id == company.Id);
            _companies[index] = company;
        }

        public int GetCount()
        {
            return _companies.Count;
        }

        public bool CnpjExists(Cnpj cnpj, Guid id)
        {
            Company company = GetByCnpj(cnpj.Number);

            if (company != null)
            {
                if (company.Id != id)
                    return true;
            }

            return false;
        }
    }
}
