namespace Ledger.Shared.ValueObjects
{
    public class Rg : ValueObject<Rg>
    {
        public string Number { get; private set; }

        protected Rg() { }

        public Rg(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
