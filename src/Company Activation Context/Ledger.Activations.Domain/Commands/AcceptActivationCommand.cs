using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Activations.Domain.Commands
{
    public class AcceptActivationCommand : Command
    {
        public Guid ActivationId { get; set; }

        public override void Validate()
        {
            new ValidationContract<AcceptActivationCommand, Guid>(this, command => command.ActivationId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
