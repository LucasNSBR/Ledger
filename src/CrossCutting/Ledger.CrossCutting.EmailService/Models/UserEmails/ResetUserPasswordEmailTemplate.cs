namespace Ledger.CrossCutting.EmailService.Models.UserEmails
{
    public class ResetUserPasswordEmailTemplate : EmailTemplate
    {
        public string ResetToken { get; }

        public ResetUserPasswordEmailTemplate(string to, string templateId, string resetToken) : base(to, templateId)
        {
            ResetToken = resetToken;

            AddSendGridSubstitution("-code-", resetToken);
        }
    }
}
