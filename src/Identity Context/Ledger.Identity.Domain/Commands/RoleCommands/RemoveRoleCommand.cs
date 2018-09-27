using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.RoleCommands
{
    public class RemoveRoleCommand : Command
    {
        public string RoleName { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveRoleCommand, string>(this, command => command.RoleName)
                 .NotEmpty()
                 .Build()
                 .AddToNotifier(this);
        }
    }
}
