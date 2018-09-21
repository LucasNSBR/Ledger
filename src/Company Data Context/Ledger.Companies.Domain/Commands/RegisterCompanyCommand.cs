using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Companies.Domain.Commands
{
    public class RegisterCompanyCommand : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string OwnerName { get; set; }
        public DateTime OwnerBirthday { get; set; }
        public string OwnerCpf { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.Name)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.Email)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(150)
                .Email()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.Description)
                .MaxLength(2000)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.Cnpj)
                .NotEmpty()
                .ExactlyLength(14)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.InscricaoEstadual)
                .NotEmpty()
                .MaxLength(16)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.OwnerName)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(120)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, DateTime>(this, command => command.OwnerBirthday)
                .GreaterOrEqualThan(DateTime.Now.AddYears(-80))
                .LessOrEqualThan(DateTime.Now.AddYears(-17))
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterCompanyCommand, string>(this, command => command.OwnerCpf)
                .NotEmpty()
                .ExactlyLength(11);
        }
    }
}
