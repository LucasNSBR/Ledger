using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class AttachIssuePictureCommand : Command
    {
        public string IssuePicture { get; set; }

        public override void Validate()
        {
            string base64RegexPattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";

            new ValidationContract<AttachIssuePictureCommand, string>(this, command => command.IssuePicture)
                .NotEmpty()
                .Regex(base64RegexPattern)
                .Build()
                .AddToNotifier(this);
        }
    }
}
