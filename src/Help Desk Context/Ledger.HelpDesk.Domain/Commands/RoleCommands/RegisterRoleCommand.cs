using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.HelpDesk.Domain.Commands.RoleCommands
{
    public class RegisterRoleCommand : Command
    {
        public string Name { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterRoleCommand, string>(this, command => command.Name)
                .NotEmpty()
                .MinLength(3)
                .MaxLength(50)
                .Build()
                .AddToNotifier(this);
        }
    }
}
