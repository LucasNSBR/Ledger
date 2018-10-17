using Ledger.Shared.Events;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserResetedPasswordEvent : DomainEvent
    {
        public string Email { get; }

        public UserResetedPasswordEvent(string email)
        {
            Email = email;
        }
    }
}
