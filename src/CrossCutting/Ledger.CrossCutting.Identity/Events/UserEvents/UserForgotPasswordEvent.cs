using Ledger.Shared.Events;
using System;

namespace Ledger.CrossCutting.Identity.Events.UserEvents
{
    public class UserForgotPasswordEvent : DomainEvent
    {
        public Guid Id { get; }
        public string Email { get; }
        public string PasswordResetToken { get; }

        public UserForgotPasswordEvent(Guid id, string email, string passwordResetToken)
        {
            Id = id;
            Email = email;
            PasswordResetToken = passwordResetToken;
        }
    }
}
