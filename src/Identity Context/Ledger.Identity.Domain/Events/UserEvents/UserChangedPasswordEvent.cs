using Ledger.Shared.Events;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserChangedPasswordEvent : DomainEvent
    {
        public string Email { get; }

        public UserChangedPasswordEvent(string email)
        {
            Email = email;
        }
    }
}
