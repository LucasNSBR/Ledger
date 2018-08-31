using Ledger.Shared.Commands;
using System;

namespace Ledger.Activations.Domain.Commands
{
    public class ResetActivationCommand : Command
    {
        public Guid ActivationId { get; set; }

        public override void Validate()
        {
        }
    }
}
