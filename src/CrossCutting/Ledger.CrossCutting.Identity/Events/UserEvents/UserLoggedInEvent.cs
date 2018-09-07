using System;

namespace Ledger.CrossCutting.Identity.Events.UserEvents
{
    public class UserLoggedInEvent
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
