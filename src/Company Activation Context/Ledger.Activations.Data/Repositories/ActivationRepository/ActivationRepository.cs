using Ledger.Activations.Data.Context;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.Activations.Domain.Specifications.ActivationSpecifications;
using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Activations.Data.Repositories.ActivationRepository
{
    public class ActivationRepository : IActivationRepository
    {
        private readonly LedgerActivationDbContext _dbContext;
        private readonly DbSet<Activation> _dbSet;
        private readonly IIdentityResolver _identityResolver;

        public ActivationRepository(LedgerActivationDbContext dbContext, IIdentityResolver identityResolver)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Activations;
            _identityResolver = identityResolver;
        }

        public Activation GetById(Guid id)
        {
            ActivationIdSpecification specification = new ActivationIdSpecification(id);

            Activation activation = _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());

            //Suppress any modification on activation by returning null
            //Since all Activation operations require to GetById() the Activation
            //If UserId is different of TenantId the operation will break 
            if (activation.TenantId != _identityResolver.GetUserId())
                return null;

            return activation;
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
