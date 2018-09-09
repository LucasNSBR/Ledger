using Ledger.Shared.Commands;

namespace Ledger.Identity.Domain.Commands
{
    public class ForgotUserPasswordCommand : Command
    {
        public string Email { get; set; }
        
        public override void Validate()
        {
        }
    }
}
