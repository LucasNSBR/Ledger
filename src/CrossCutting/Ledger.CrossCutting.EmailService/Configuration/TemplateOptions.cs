namespace Ledger.CrossCutting.EmailService.Configuration
{
    public class TemplateOptions
    {
        private TemplateOptions() { }

        public string ConfirmUserAccountEmailTemplateId { get; set; }
        public string ResetUserPasswordEmailTemplateId { get; set; }
        public string UserPasswordPostResetEmailTemplateId { get; set; }
        public string UserPasswordPostChangeEmailTemplateId { get; set; }

        public string CompanyActivationAcceptedTemplateId { get; set; }
        public string CompanyActivationRejectedTemplateId { get; set; }

        public string CompanyRegisteredTemplateId { get; set; }
    }
}
