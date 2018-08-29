namespace Ledger.Shared.ValueObjects
{
    public class Rg : ValueObject<Rg>
    {
        public string NumeroRg { get; private set; }

        protected Rg() { }

        public Rg(string numeroRg)
        {
            NumeroRg = numeroRg;
        }

        public override string ToString()
        {
            return NumeroRg;
        }
    }
}
