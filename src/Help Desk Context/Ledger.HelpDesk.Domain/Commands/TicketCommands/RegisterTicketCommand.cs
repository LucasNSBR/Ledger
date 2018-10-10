using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCommands
{
    public class RegisterTicketCommand : Command
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public Guid CategoryId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterTicketCommand, string>(this, command => command.Title)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterTicketCommand, string>(this, command => command.Details)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(2000)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterTicketCommand, Guid>(this, command => command.CategoryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
