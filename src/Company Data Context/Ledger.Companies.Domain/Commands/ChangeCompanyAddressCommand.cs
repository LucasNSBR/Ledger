using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Companies.Domain.Commands
{
    public class ChangeCompanyAddressCommand : Command
    {
        public Guid CompanyId { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Complementation { get; set; }
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public string Cep { get; set; }

        public override void Validate()
        {
            new ValidationContract<ChangeCompanyAddressCommand, Guid>(this, command => command.CompanyId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, int>(this, command => command.Number)
                .Between(0, 10000)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, string>(this, command => command.Street)
                .NotEmpty()
                .MinLength(3)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, string>(this, command => command.Neighborhood)
                .NotEmpty()
                .MinLength(3)
                .MaxLength(100)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, string>(this, command => command.Complementation)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, Guid>(this, command => command.CityId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, Guid>(this, command => command.StateId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, Guid>(this, command => command.CountryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<ChangeCompanyAddressCommand, string>(this, command => command.Cep)
                .ExactlyLength(8)
                .Build()
                .AddToNotifier(this);
        }
    }
}
