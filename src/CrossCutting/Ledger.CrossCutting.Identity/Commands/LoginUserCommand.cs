using Ledger.Shared.Commands;

namespace Ledger.CrossCutting.Identity.Commands
{
    public class LoginUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
        }
    }
}
