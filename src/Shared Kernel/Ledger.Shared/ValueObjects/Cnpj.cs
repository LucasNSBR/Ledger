namespace Ledger.Shared.ValueObjects
{
    //CNPJ IS EQUIVALENTE TO EIN (EMPLOYER IDENTIFICATION NUMBER)
    public class Cnpj : ValueObject<Cnpj>
    {
        public string Number { get; private set; }

        protected Cnpj() { }

        public Cnpj(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
