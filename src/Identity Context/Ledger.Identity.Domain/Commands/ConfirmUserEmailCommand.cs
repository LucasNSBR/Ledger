using Ledger.Shared.Commands;

namespace Ledger.Identity.Domain.Commands
{
    public class ConfirmUserEmailCommand : Command
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public override void Validate()
        {
        }
    }
}
