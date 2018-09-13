namespace Ledger.CrossCutting.EmailService.Models.CompanyEmails
{
    public class CompanyRegisteredEmailTemplate : EmailTemplate
    {
        public CompanyRegisteredEmailTemplate(string to, string templateId) : base(to, templateId)
        {
        }
    }
}
