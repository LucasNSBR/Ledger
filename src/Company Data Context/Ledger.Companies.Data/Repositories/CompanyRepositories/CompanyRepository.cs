using Ledger.Companies.Data.Context;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

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
            return null;
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
