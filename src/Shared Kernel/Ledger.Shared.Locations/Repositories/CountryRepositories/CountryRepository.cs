using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Locations.Context;
using Ledger.Shared.Locations.Specifications.CountrySpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.CountryRepositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly LedgerLocationDbContext _dbContext;
        private readonly DbSet<Country> _dbSet;

        public CountryRepository(LedgerLocationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Countries;
        }

        public IQueryable<Country> GetAllCountries()
        {
            return _dbSet
                .AsNoTracking();
        }

        public IQueryable<Country> GetByName(string name)
        {
            CountryNameSpecification specification = new CountryNameSpecification(name);

            return _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());
        }

        public Country GetById(Guid id)
        {
            CountryIdSpecification specification = new CountryIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }
    }
}
