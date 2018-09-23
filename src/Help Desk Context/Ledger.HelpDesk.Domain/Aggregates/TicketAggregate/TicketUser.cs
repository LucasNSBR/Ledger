using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketUser : Entity<TicketUser>
    {
        public TicketUser(Guid userId)
        {
            Id = userId;
        }
    }
}
