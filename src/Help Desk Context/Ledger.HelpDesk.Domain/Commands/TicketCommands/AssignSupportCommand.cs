using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class AssignSupportCommand : Command
    {
        public Guid TicketId { get; set; }

        public override void Validate()
        {
            new ValidationContract<AssignSupportCommand, Guid>(this, command => command.TicketId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
