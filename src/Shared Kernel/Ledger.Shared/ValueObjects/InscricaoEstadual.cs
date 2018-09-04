namespace Ledger.Shared.ValueObjects
{
    public class InscricaoEstadual : ValueObject<InscricaoEstadual>
    {
        public string Number { get; private set; }

        protected InscricaoEstadual() { }

        public InscricaoEstadual(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
