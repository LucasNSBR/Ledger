namespace Ledger.CrossCutting.EmailService.Models.UserEmails
{
    public class UserPasswordPostChangeEmailTemplate : EmailTemplate
    {
        public UserPasswordPostChangeEmailTemplate(string to, string templateId) : base(to, templateId)
        {
        }
    }
}
