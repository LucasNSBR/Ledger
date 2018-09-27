using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class AssignSupportUserCommand : Command
    {
        public Guid UserId { get; set; }

        public override void Validate()
        {
            new ValidationContract<AssignSupportUserCommand, Guid>(this, command => command.UserId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
