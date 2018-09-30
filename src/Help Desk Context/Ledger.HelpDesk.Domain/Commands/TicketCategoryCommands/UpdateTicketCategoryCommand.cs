using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands
{
    public class UpdateTicketCategoryCommand : Command
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public override void Validate()
        {
            new ValidationContract<UpdateTicketCategoryCommand, Guid>(this, command => command.CategoryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<UpdateTicketCategoryCommand, string>(this, command => command.Name)
                .NotEmpty()
                .MinLength(3)
                .MaxLength(100)
                .Build()
                .AddToNotifier(this);
        }
    }
}
