using System;
using System.Linq;
using Ledger.Activations.Data.Context;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.Activations.Domain.Specifications.ActivationSpecifications;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Activations.Data.Repositories.ActivationRepository
{
    public class ActivationRepository : IActivationRepository
    {
        private readonly LedgerActivationDbContext _dbContext;
        private readonly DbSet<Activation> _dbSet;

        public ActivationRepository(LedgerActivationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Activations;
        }

        public Activation GetById(Guid id)
        {
            ActivationIdSpecification specification = new ActivationIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(Activation activation)
        {
            _dbContext.Add(activation);
        }

        public void Update(Activation activation)
        {
            _dbContext.Update(activation);
        }
    }
}
