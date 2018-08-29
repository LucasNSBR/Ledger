namespace Ledger.Shared.ValueObjects
{
    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public string Number { get; private set; }

        protected PhoneNumber() { }

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
