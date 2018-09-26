using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.RoleCommands
{
    public class RemoveRoleCommand : Command
    {
        public Guid RoleId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveRoleCommand, Guid>(this, command => command.RoleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
