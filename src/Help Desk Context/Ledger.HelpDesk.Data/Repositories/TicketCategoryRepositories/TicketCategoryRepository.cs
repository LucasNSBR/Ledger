using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Domain.Specifications.TicketCategorySpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Data.Repositories.TicketCategoryRepositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly LedgerHelpDeskDbContext _dbContext;
        private readonly DbSet<TicketCategory> _dbSet;

        public TicketCategoryRepository(LedgerHelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.TicketCategories;
        }

        public IQueryable<TicketCategory> GetAllCategories()
        {
            return _dbSet
                .AsNoTracking();
        }

        public TicketCategory GetById(Guid id)
        {
            TicketCategoryIdSpecification specification = new TicketCategoryIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(TicketCategory category)
        {
            _dbContext.Add(category);
        }

        public void Update(TicketCategory category)
        {
            _dbContext.Update(category);
        }
    }
}
