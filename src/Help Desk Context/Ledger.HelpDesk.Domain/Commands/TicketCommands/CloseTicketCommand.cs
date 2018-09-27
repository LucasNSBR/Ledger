using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class CloseTicketCommand : Command
    {
        public Guid TicketId { get; set; }

        public override void Validate()
        {
            new ValidationContract<CloseTicketCommand, Guid>(this, command => command.TicketId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
