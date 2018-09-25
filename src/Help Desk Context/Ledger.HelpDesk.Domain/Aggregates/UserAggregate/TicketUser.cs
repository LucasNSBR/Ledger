using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public class TicketUser : User
    {
        public TicketUser(Guid id, string email) : base(id, email)
        {
        }
    }
}
