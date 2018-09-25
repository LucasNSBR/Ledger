using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands
{
    public class AddUserSupportRoleCommand : Command
    {
        public string Email { get; set; }

        public override void Validate()
        {
            new ValidationContract<AddUserSupportRoleCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);
        }
    }
}
