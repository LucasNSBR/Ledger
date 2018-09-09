using Ledger.Shared.Events;
using System;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserLoggedInEvent : DomainEvent
    {
        public Guid Id { get; }
        public string Email { get; }
        public DateTime LoginDate { get; }

        public UserLoggedInEvent(Guid id, string email)
        {
            Id = id;
            LoginDate = DateTime.Now;
            Email = email;
        }
    }
}
