using Ledger.Shared.Commands;

namespace Ledger.CrossCutting.Identity.Commands
{
    public class ResetUserPasswordCommand : Command
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }

        public override void Validate()
        {
        }
    }
}
