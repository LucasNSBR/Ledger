namespace Ledger.Shared.ValueObjects
{
    //CPF IS EQUIVALENT TO SSN (SOCIAL SECURITY NUMBER)
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
