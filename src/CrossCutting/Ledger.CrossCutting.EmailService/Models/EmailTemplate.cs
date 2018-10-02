using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace Ledger.CrossCutting.EmailService.Models
{
    public class EmailTemplate
    {
        public EmailAddress To { get; private set; }
        public string TemplateId { get; private set; }

        private readonly Dictionary<string, string> _sendGridSubstitutions;
        public IReadOnlyDictionary<string, string> SendGridSubstitutions
        {
            get
            {
                return _sendGridSubstitutions;
            }
        }

        public EmailTemplate(string to)
        {
            _sendGridSubstitutions = new Dictionary<string, string>();
            To = new EmailAddress(to);         

            AddSubstitution("-to-", to);
        }

        /// <summary>
        /// Set SendGrid Email Template Id.
        /// Please visit https://sendgrid.com/solutions/transactional-email-templates/
        /// </summary>
        /// <param name="templateId">Id of SendGrid template</param>
        /// <returns></returns>
        public EmailTemplate SetTemplate(string templateId)
        {
            TemplateId = templateId;

            return this;
        }

        /// <summary>
        /// Add substitution key to be replaced in SendGrid Email template.
        /// Please visit https://sendgrid.com/docs/API_Reference/SMTP_API/substitution_tags.html
        /// </summary>
        /// <param name="key">The key to be replaced</param>
        /// <param name="value">The value that will replace the key</param>
        /// <returns></returns>
        public EmailTemplate AddSubstitution(string key, string value)
        {
            if (_sendGridSubstitutions.ContainsKey(key))
                _sendGridSubstitutions[key] = value;
            else
                _sendGridSubstitutions.Add(key, value);

            return this;
        }
    }
}