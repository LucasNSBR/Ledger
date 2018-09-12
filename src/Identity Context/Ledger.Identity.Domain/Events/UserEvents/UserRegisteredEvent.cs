using Ledger.Shared.Events;
using System;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string Email { get; }
        public DateTime RegistrationDate { get; }
        public string EmailConfirmationToken { get; }

        public UserRegisteredEvent(string email, string emailConfirmationToken)
        {
            Email = email;
            RegistrationDate = DateTime.Now;
            EmailConfirmationToken = emailConfirmationToken;
        }
    }
}
