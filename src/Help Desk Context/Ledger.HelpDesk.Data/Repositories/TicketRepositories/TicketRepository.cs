using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using Ledger.HelpDesk.Domain.Specifications.TicketSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Data.Repositories.TicketRepositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly LedgerHelpDeskDbContext _dbContext;
        private readonly DbSet<Ticket> _dbSet;

        public TicketRepository(LedgerHelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Tickets;
        }

        public IQueryable<Ticket> GetByUserId(Guid userId)
        {
            TicketUserIdSpecification specification = new TicketUserIdSpecification(userId);

            return _dbSet
                .AsNoTracking()
                .Include(c => c.Conversation)
                .ThenInclude(tc => tc.Messages)
                .Where(specification.ToExpression());
        }

        public Ticket GetById(Guid id)
        {
            TicketIdSpecification specification = new TicketIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .Include(c => c.Conversation)
                .ThenInclude(tc => tc.Messages)
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(Ticket ticket)
        {
            _dbContext.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            _dbContext.Update(ticket);
        }
    }
}
