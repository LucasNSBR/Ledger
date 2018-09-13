namespace Ledger.CrossCutting.EmailService.Models.ActivationEmails
{
    public class CompanyActivationAcceptedEmailTemplate : EmailTemplate
    {
        public CompanyActivationAcceptedEmailTemplate(string to, string templateId) : base(to, templateId)
        {
        }
    }
}
