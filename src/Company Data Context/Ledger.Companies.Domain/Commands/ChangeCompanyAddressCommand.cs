using Ledger.Shared.Commands;
using System;

namespace Ledger.Companies.Domain.Commands
{
    public class ChangeCompanyAddressCommand : Command
    {
        public Guid CompanyId { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public override void Validate()
        {
        }
    }
}
