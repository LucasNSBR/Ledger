using Ledger.Shared.Entities.Locations;
using Ledger.Shared.Locations.Context;
using Ledger.Shared.Locations.Specifications.StateSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.StateRepositories
{
    public class StateRepository : IStateRepository
    {
        private readonly LedgerLocationDbContext _dbContext;
        private readonly DbSet<State> _dbSet;

        public StateRepository(LedgerLocationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.States;
        }

        public IQueryable<State> GetByCountry(Guid id)
        {
            StateCountryIdSpecification specification = new StateCountryIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());
        }

        public State GetById(Guid id)
        {
            StateIdSpecification specification = new StateIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }
    }
}
