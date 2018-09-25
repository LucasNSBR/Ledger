using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public class TicketUser : User
    {
        protected TicketUser() { }

        public TicketUser(Guid id, string email) : base(id, email)
        {
        }
    }
}
