using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class ConfirmUserEmailCommand : Command
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public override void Validate()
        {
            new ValidationContract<ConfirmUserEmailCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ConfirmUserEmailCommand, string>(this, command => command.ConfirmationToken)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
