namespace Ledger.Shared.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public int Number { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }

        //Notes or extra details about this address
        public string Complementation { get; private set; }

        //Since names never tend to change, Address will keep just weak references 
        //If name changes, this need to be changed manually on Database or by every user
        //Very uncommon
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        //Same as ZipCode
        public string Cep { get; private set; }

        protected Address() { }

        public Address(int number, string street, string neighborhood, string complementation, string city, string state, string country, string cep)
        {
            Number = number;
            Street = street;
            Neighborhood = neighborhood;
            Complementation = complementation;
            City = city;
            State = state;
            Country = country;
            Cep = cep;
        }

        public override string ToString()
        {
            return $"{Street}, {Number}, {Neighborhood} - {City}, {Cep}, {State} - {Country}";
        }
    }
}
