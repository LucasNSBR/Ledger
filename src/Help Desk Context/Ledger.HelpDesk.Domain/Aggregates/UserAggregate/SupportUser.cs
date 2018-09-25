using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public class SupportUser : User
    {
        public SupportUser(Guid id, string email) : base(id, email)
        {
        }
    }
}
