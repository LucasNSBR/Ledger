using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.EmailService.Services.Dispatchers
{
    public class EmailDispatcher : IEmailDispatcher
    {
        private readonly string _sendGridKey;
        private readonly string _sendGridUser;
        private readonly EmailAddress _senderAddress;

        public EmailDispatcher(IOptions<DispatcherOptions> options)
        {
            _sendGridKey = options.Value.SendGridKey;
            _sendGridUser = options.Value.SendGridUser;

            _senderAddress = new EmailAddress(options.Value.SendAddress, options.Value.SenderName);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            SendGridMessage message = new SendGridMessage();

            message.SetFrom(_senderAddress);
            message.AddTo(to);
            message.SetSubject(subject);
            message.AddContent("text/plain", body);

            await Dispatch(message);
        }

        public async Task SendEmailAsync(EmailTemplate template)
        {
            SendGridMessage message = new SendGridMessage();

            message.SetFrom(_senderAddress);
            message.AddTo(template.To);
            message.SetTemplateId(template.TemplateId);
            message.AddSubstitutions(template.SendGridSubstitutions.ToDictionary(k => k.Key, v => v.Value));

            await Dispatch(message);
        }

        private async Task Dispatch(SendGridMessage message)
        {
            SendGridClient client = new SendGridClient(_sendGridKey);
            await client.SendEmailAsync(message);
        }
    }
}
