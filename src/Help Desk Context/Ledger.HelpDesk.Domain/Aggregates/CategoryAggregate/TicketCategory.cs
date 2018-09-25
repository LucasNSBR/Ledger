using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate
{
    public class TicketCategory : Entity<TicketCategory>, IAggregateRoot
    {
        public string Name { get; private set; }

        protected TicketCategory() { }

        public TicketCategory(string name)
        {
            Name = name;
        }

        public TicketCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
