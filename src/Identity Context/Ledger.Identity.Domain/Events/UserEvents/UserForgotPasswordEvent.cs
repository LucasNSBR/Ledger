using Ledger.Shared.Events;

namespace Ledger.Identity.Domain.Events.UserEvents
{
    public class UserForgotPasswordEvent : DomainEvent
    {
        public string Email { get; }
        public string PasswordResetToken { get; }

        public UserForgotPasswordEvent(string email, string passwordResetToken)
        {
            Email = email;
            PasswordResetToken = passwordResetToken;
        }
    }
}
