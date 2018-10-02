using Ledger.Shared.Entities.Locations;

namespace Ledger.Shared.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public int Number { get; private set; }
        public string Street { get; private set; }
        public string Neighborhood { get; private set; }

        //Notes or extra details about this address
        public string Complementation { get; private set; }

        public City City { get; private set; }
        public State State { get; private set; }
        public Country Country { get; private set; }

        //Same as ZipCode
        public string Cep { get; private set; }

        protected Address() { }
        
        public Address(int number, string street, string neighborhood, string complementation, City city, State state, Country country, string cep)
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
            return $"{Street}, {Number}, {Neighborhood} - {City.Name}, {Cep}, {City.State.Name}";
        }
    }
}
