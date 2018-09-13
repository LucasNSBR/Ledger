using Ledger.Companies.Data.Context;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Repositories;
using Ledger.Companies.Domain.Specifications.CompanySpecifications;
using Ledger.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Companies.Data.Repositories.CompanyRepositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly LedgerCompanyDbContext _dbContext;
        private readonly DbSet<Company> _dbSet;

        public CompanyRepository(LedgerCompanyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Companies;
        }

        public Company GetById(Guid id)
        {
            CompanyIdSpecification specification = new CompanyIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public Company GetByCnpj(string cnpj)
        {
            CompanyCnpjSpecification specification = new CompanyCnpjSpecification(cnpj);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
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

        public void Register(Company company)
        {
            _dbContext.Add(company);
        }

        public void Update(Company company)
        {
            _dbContext.Update(company);
        }
    }
}
