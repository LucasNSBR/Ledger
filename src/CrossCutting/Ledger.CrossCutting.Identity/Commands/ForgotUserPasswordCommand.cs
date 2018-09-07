using Ledger.Shared.Commands;

namespace Ledger.CrossCutting.Identity.Commands
{
    public class ForgotUserPasswordCommand : Command
    {
        public string Email { get; set; }
        
        public override void Validate()
        {
        }
    }
}
