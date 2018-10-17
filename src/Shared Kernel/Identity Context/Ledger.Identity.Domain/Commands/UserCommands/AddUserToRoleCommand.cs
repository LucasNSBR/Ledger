using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class AddUserToRoleCommand : Command
    {
        public string Email { get; set; }
        public string RoleName { get; set; }

        public override void Validate()
        {
            new ValidationContract<AddUserToRoleCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AddUserToRoleCommand, string>(this, command => command.RoleName)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
