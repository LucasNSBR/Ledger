using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Companies.Domain.Commands
{
    public class ChangeCompanyPhoneCommand : Command
    {
        public Guid CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        
        public override void Validate()
        {
            new ValidationContract<ChangeCompanyPhoneCommand, Guid>(this, command => command.CompanyId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyPhoneCommand, string>(this, command => command.PhoneNumber)
                .NotEmpty()
                .MinLength(8)
                .MaxLength(14)
                .Build()
                .AddToNotifier(this);
        }
    }
}
