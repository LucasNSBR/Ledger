namespace Ledger.Shared.ValueObjects
{
    public class Cnpj : ValueObject<Cnpj>
    {
        public string NumeroCnpj { get; private set; }

        protected Cnpj() { }

        public Cnpj(string numeroCnpj)
        {
            NumeroCnpj = numeroCnpj;
        }

        public override string ToString()
        {
            return NumeroCnpj;
        }
    }
}
