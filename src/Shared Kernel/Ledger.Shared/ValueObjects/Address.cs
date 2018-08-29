namespace Ledger.Shared.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public int Number { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }

        public string City { get; private set; }
        public string State { get; private set; }

        //É o mesmo que CEP
        public string ZipCode { get; private set; }

        public Address() { }

        public Address(int number, string street, string neighborhood, string city, string state, string zipCode)
        {
            Number = number;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}, {Number}, {Neighborhood} - {City}, {ZipCode}, {State}";
        }
    }
}
