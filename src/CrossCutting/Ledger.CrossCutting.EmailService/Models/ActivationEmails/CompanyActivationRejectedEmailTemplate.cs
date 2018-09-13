namespace Ledger.CrossCutting.EmailService.Models.ActivationEmails
{
    public class CompanyActivationRejectedEmailTemplate : EmailTemplate
    {
        public CompanyActivationRejectedEmailTemplate(string to, string templateId) : base(to, templateId)
        {
        }
    }
}
