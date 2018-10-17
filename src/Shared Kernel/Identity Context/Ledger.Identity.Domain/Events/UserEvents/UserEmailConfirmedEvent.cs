using Ledger.Shared.Events;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserEmailConfirmedEvent : DomainEvent
    {
        public string Email { get; }

        public UserEmailConfirmedEvent(string email)
        {
            Email = email;
        }
    }
}
