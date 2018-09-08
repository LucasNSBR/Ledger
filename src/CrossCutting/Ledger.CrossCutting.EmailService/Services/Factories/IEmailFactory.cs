using Ledger.CrossCutting.EmailService.Models;

namespace Ledger.CrossCutting.EmailService.Services.Factories
{
    public interface IEmailFactory
    {
        EmailTemplate CreateAccountConfirmationEmail(string to, string confirmationToken);
        EmailTemplate CreatePasswordResetEmail(string to, string resetToken);
    }
}
