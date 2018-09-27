using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Models.ActivationEmails;
using Ledger.CrossCutting.EmailService.Models.CompanyEmails;
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

        private readonly string _companyActivationAcceptedTemplateId;
        private readonly string _companyActivationRejectedTemplateId;

        private readonly string _companyRegisteredTemplateId;

        public EmailFactory(IOptions<TemplateOptions> options)
        {
            _confirmUserAccountEmailTemplateId = options.Value.ConfirmUserAccountEmailTemplateId;
            _resetUserPasswordEmailTemplateId = options.Value.ResetUserPasswordEmailTemplateId;
            _userPasswordPostResetEmailTemplateId = options.Value.UserPasswordPostResetEmailTemplateId;
            _userPasswordPostChangeEmailTemplateId = options.Value.UserPasswordPostChangeEmailTemplateId;

            _companyActivationAcceptedTemplateId = options.Value.CompanyActivationAcceptedTemplateId;
            _companyActivationRejectedTemplateId = options.Value.CompanyActivationRejectedTemplateId;

            _companyRegisteredTemplateId = options.Value.CompanyRegisteredTemplateId;
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

        public EmailTemplate CreateCompanyActivationAcceptedEmail(string to)
        {
            CompanyActivationAcceptedEmailTemplate template =
                new CompanyActivationAcceptedEmailTemplate(to, _companyActivationAcceptedTemplateId);

            return template;
        }

        public EmailTemplate CreateCompanyActivationRejectedEmail(string to)
        {
            CompanyActivationRejectedEmailTemplate template =
                new CompanyActivationRejectedEmailTemplate(to, _companyActivationRejectedTemplateId);

            return template;
        }

        public EmailTemplate CreateCompanyRegisteredEmail(string to)
        {
            CompanyRegisteredEmailTemplate template =
                new CompanyRegisteredEmailTemplate(to, _companyRegisteredTemplateId);

            return template;
        }
    }
}
