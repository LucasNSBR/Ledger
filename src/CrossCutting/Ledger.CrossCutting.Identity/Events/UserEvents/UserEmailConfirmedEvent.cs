using Ledger.Shared.Events;

namespace Ledger.CrossCutting.Identity.Events.UserEvents
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
