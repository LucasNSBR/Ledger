using Ledger.Shared.Commands;
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
        }
    }
}
