using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices
{
    public interface ITicketCategoryApplicationService
    {
        IQueryable<TicketCategory> GetAllCategories();
        TicketCategory GetById(Guid id);
        void Register(RegisterTicketCategoryCommand command);
        void Update(UpdateTicketCategoryCommand command);
    }
}
