using Ledger.Shared.Commands;

namespace Ledger.Identity.Domain.Commands
{
    public class ChangeUserPasswordCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public override void Validate()
        {
        }
    }
}
