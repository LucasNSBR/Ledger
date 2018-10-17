using Ledger.Shared.Events;
using System;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserLoggedInEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public DateTime LoginDate { get; }

        public UserLoggedInEvent(Guid userId, string email)
        {
            UserId = userId;
            LoginDate = DateTime.Now;
            Email = email;
        }
    }
}
