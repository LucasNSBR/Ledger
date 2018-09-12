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
        private readonly string _userPasswordPostResetEmailTemplateId;
        private readonly string _userPasswordPostChangeEmailTemplateId;

        public EmailFactory(IOptions<TemplateOptions> options)
        {
            _confirmUserAccountEmailTemplateId = options.Value.ConfirmUserAccountEmailTemplateId;
            _resetUserPasswordEmailTemplateId = options.Value.ResetUserPasswordEmailTemplateId;
            _userPasswordPostResetEmailTemplateId = options.Value.UserPasswordPostResetEmailTemplateId;
            _userPasswordPostChangeEmailTemplateId = options.Value.UserPasswordPostChangeEmailTemplateId;
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

        public EmailTemplate CreatePostPasswordResetEmail(string to)
        {
            UserPasswordPostResetEmailTemplate template =
                new UserPasswordPostResetEmailTemplate(to, _userPasswordPostResetEmailTemplateId);

            return template;
        }

        public EmailTemplate CreatePostPasswordChangeEmail(string to)
        {
            UserPasswordPostChangeEmailTemplate template =
                new UserPasswordPostChangeEmailTemplate(to, _userPasswordPostChangeEmailTemplateId);

            return template;
        }
    }
}
