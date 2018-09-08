using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Models.UserEmails;
using Microsoft.Extensions.Options;

namespace Ledger.CrossCutting.EmailService.Services.Factories
{
    public class EmailFactory : IEmailFactory
    {
        private readonly string _confirmUserAccountEmailTemplateId;
        private readonly string _resetUserPasswordEmailTemplateId;

        public EmailFactory(IOptions<TemplateOptions> options)
        {
            _confirmUserAccountEmailTemplateId = options.Value.ConfirmUserAccountEmailTemplateId;
            _resetUserPasswordEmailTemplateId = options.Value.ResetUserPasswordEmailTemplateId;
        }

        public EmailTemplate CreateAccountConfirmationEmail(string to, string confirmationToken)
        {
            ConfirmUserAccountEmailTemplate template = 
                new ConfirmUserAccountEmailTemplate(to, _confirmUserAccountEmailTemplateId, confirmationToken);

            return template;
        }

        public EmailTemplate CreatePasswordResetEmail(string to, string resetToken)
        {
            ResetUserPasswordEmailTemplate template = 
                new ResetUserPasswordEmailTemplate(to, _resetUserPasswordEmailTemplateId, resetToken);

            return template;
        }
    }
}
