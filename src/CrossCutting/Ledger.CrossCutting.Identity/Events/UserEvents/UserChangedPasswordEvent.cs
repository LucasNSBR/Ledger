using System;

namespace Ledger.CrossCutting.Identity.Events.UserEvents
{
    public class UserChangedPasswordEvent
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
