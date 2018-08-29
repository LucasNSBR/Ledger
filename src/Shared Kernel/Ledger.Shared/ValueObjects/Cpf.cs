namespace Ledger.Shared.ValueObjects
{
    public class Cpf : ValueObject<Cpf>
    {
        public string NumeroCpf { get; private set; }

        protected Cpf() { }

        public Cpf(string numeroCpf)
        {
            NumeroCpf = numeroCpf;
        }

        public override string ToString()
        {
            return NumeroCpf;
        }
    }
}
