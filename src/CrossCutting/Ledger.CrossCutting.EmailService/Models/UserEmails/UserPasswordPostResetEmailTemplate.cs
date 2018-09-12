namespace Ledger.CrossCutting.EmailService.Models.UserEmails
{
    public class UserPasswordPostResetEmailTemplate : EmailTemplate
    {
        public UserPasswordPostResetEmailTemplate(string to, string templateId) : base(to, templateId)
        {
        }
    }
}
