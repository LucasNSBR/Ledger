using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class AddMessageCommand : Command
    {
        public string Body { get; set; }
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        
        public override void Validate()
        {
            new ValidationContract<AddMessageCommand, string>(this, command => command.Body)
                .NotEmpty()
                .MinLength(1)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AddMessageCommand, Guid>(this, command => command.TicketId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AddMessageCommand, Guid>(this, command => command.UserId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
