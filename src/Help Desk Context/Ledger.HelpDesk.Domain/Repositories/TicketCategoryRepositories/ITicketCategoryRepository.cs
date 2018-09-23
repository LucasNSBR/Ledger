using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories
{
    public interface ITicketCategoryRepository
    {
        IQueryable<TicketCategory> GetAllCategories();
        TicketCategory GetById(Guid id);
        void Register(TicketCategory category);
        void Update(TicketCategory category);
    }
}
