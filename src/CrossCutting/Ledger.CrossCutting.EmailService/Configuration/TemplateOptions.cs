namespace Ledger.CrossCutting.EmailService.Configuration
{
    public class TemplateOptions
    {
        public string ConfirmUserAccountEmailTemplateId { get; set; }
        public string ResetUserPasswordEmailTemplateId { get; set; }
        public string UserPasswordPostResetEmailTemplateId { get; set; }
        public string UserPasswordPostChangeEmailTemplateId { get; set; }
    }
}
