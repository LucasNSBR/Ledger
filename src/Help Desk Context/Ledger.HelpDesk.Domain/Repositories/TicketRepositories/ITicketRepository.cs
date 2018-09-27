using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Repositories.TicketRepositories
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> GetByUserId(Guid userId);
        Ticket GetById(Guid id);
        void Register(Ticket ticket);
        void Update(Ticket ticket);
    }
}
