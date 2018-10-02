using System;

namespace Ledger.Shared.Entities.Locations
{
    public class State : Entity<State>
    {
        public string ShortName { get; private set; }
        public string Name { get; private set; }
        public Guid CountryId { get; private set; }
        public Country Country { get; private set; }

        public State(string shortName, string name, Country country)
        {
            ShortName = shortName;
            Name = name;
            Country = country;
        }
    }
}
