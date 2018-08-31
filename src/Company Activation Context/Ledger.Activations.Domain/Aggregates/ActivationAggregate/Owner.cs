using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Owner : ValueObject<Owner>
    {
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public Cpf Cpf { get; private set; }
        
        protected Owner() { }

        public Owner(string name, DateTime birthday, Cpf cpf)
        {
            Name = name;
            Birthday = birthday;
            Cpf = cpf;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
