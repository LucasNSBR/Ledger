using Ledger.Shared.ValueObjects;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Owner : ValueObject<Owner>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public Cpf Cpf { get; private set; }
        public byte[] DocumentPicture { get; private set; }

        protected Owner() { }

        public Owner(string name, int age, Cpf cpf, byte[] documentPicture)
        {
            Name = name;
            Age = age;
            Cpf = cpf;
            DocumentPicture = documentPicture;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
