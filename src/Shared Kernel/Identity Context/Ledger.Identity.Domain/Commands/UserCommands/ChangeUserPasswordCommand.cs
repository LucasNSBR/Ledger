using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class ChangeUserPasswordCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public override void Validate()
        {
            new ValidationContract<ChangeUserPasswordCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeUserPasswordCommand, string>(this, command => command.Password)
                .NotEmpty()
                .MinLength(8)
                .MaxLength(24)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeUserPasswordCommand, string>(this, command => command.NewPassword)
                .NotEmpty()
                .MinLength(8)
                .MaxLength(24)
                .Build()
                .AddToNotifier(this);
        }
    }
}
