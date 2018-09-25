using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public abstract class User : Entity<User>, IAggregateRoot
    {
        public string Email { get; private set; }

        protected User() { }

        public User(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
