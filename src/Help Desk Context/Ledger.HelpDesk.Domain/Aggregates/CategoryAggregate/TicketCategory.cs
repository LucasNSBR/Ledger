using Ledger.Shared.Entities;

namespace Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate
{
    public class TicketCategory : Entity<TicketCategory>, IAggregateRoot
    {
        public string Name { get; private set; }

        public TicketCategory(string name)
        {
            Name = name;
        }
    }
}
