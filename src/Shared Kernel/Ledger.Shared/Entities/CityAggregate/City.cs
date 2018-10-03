using System;

namespace Ledger.Shared.Entities.CityAggregate
{
    public class City : Entity<City>
    {
        public string Name { get; private set; }
        public Guid StateId { get; private set; }

        public City(string name, Guid stateId)
        {
            Name = name;
            StateId = stateId;
        }
    }
}
