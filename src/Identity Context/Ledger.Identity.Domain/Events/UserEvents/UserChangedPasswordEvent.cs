using Ledger.Shared.Events;
using System;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserChangedPasswordEvent : DomainEvent
    {
        public Guid Id { get; }
        public string Email { get; }

        public UserChangedPasswordEvent(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
