using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Locations.Context;
using Ledger.Shared.Locations.Specifications.CitySpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.CityRepositories
{
    public class CityRepository : ICityRepository
    {
        private readonly LedgerLocationDbContext _dbContext;
        private readonly DbSet<City> _dbSet;

        public CityRepository(LedgerLocationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Cities;
        }

        public City GetById(Guid id)
        {
            CityIdSpecification specification = new CityIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public IQueryable<City> GetByName(string name)
        {
            CityNameSpecification specification = new CityNameSpecification(name);

            return _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());
        }

        public IQueryable<City> GetByState(Guid stateId)
        {
            CityStateIdSpecification specification = new CityStateIdSpecification(stateId);

            return _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());
        }
    }
}
