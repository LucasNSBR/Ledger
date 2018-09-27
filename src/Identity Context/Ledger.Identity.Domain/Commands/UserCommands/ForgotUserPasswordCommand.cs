using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class ForgotUserPasswordCommand : Command
    {
        public string Email { get; set; }
        
        public override void Validate()
        {
            new ValidationContract<ForgotUserPasswordCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);
        }
    }
}
