using Ledger.Shared.Events;
using System;

namespace Ledger.CrossCutting.Identity.Events.UserEvents
{
    public class UserRegisteredEvent : DomainEvent
    {
        public Guid Id { get; }
        public string Email { get; }
        public DateTime RegistrationDate { get; }
        public string EmailConfirmationToken { get; }

        public UserRegisteredEvent(Guid id, string email, string emailConfirmationToken)
        {
            Id = id;
            Email = email;
            RegistrationDate = DateTime.Now;
            EmailConfirmationToken = emailConfirmationToken;
        }
    }
}
