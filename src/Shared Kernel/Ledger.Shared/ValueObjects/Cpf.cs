namespace Ledger.Shared.ValueObjects
{
    public class Cpf : ValueObject<Cpf>
    {
        public string Number { get; private set; }

        protected Cpf() { }

        public Cpf(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
