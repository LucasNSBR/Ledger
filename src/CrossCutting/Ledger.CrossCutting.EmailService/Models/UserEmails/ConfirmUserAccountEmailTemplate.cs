namespace Ledger.CrossCutting.EmailService.Models.UserEmails
{
    public class ConfirmUserAccountEmailTemplate : EmailTemplate
    {
        public string ConfirmationToken { get; }

        public ConfirmUserAccountEmailTemplate(string to, string templateId, string confirmationToken) : base(to, templateId)
        {
            ConfirmationToken = confirmationToken;

            AddSendGridSubstitution("-to-", to);
            AddSendGridSubstitution("-code-", confirmationToken);
        }
    }
}
