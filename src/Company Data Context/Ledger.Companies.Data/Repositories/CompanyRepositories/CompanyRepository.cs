using Ledger.Companies.Data.Context;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Repositories;
using Ledger.Companies.Domain.Specifications.CompanySpecifications;
using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;
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
        private readonly IIdentityResolver _identityResolver;

        public CompanyRepository(LedgerCompanyDbContext dbContext, IIdentityResolver identityResolver)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Companies;
            _identityResolver = identityResolver;
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

            if (company != null && company.Id != id)
                return true;

            return false;
        }

        public void Register(Company company)
        {
            company.SetTenantId(_identityResolver.GetUserId());

            _dbContext.Add(company);
        }

        public void Update(Company company)
        {
            _dbContext.Update(company);
        }
    }
}
