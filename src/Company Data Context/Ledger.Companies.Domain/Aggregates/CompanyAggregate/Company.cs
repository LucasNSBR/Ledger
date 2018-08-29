using System;

namespace Ledger.Companies.Domain.Aggregates.CompanyAggregate
{
    public class Company
    {
        public Guid Id { get; private set; }

        public bool Active { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Cnpj { get; private set; }

        public Company(string name, string description, string cnpj)
        {
            Active = false;

            Name = name;
            Description = description;
            Cnpj = cnpj;
        }

        public Company(Guid id, string name, string description, string cnpj)
        {
            Id = id;
            Name = name;
            Description = description;
            Cnpj = cnpj;
        }
    }
}
