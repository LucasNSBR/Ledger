using System;

namespace Ledger.Shared.Entities.Locations
{
    public class City : Entity<City>
    {
        public string Name { get; private set; }
        public Guid StateId { get; private set; }
        public State State { get; private set; }

        public City(string name, State state)
        {
            Name = name;
            State = state;
        }
    }
}
