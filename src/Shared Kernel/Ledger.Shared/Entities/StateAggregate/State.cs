using System;

namespace Ledger.Shared.Entities.StateAggregate
{
    public class State : Entity<State>
    {
        public string ShortName { get; private set; }
        public string Name { get; private set; }
        public Guid CountryId { get; private set; }

        public State(string shortName, string name, Guid countryId)
        {
            ShortName = shortName;
            Name = name;
            CountryId = countryId;
        }
    }
}
