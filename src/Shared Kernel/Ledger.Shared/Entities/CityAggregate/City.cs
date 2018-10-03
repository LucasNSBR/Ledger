using Ledger.Shared.Entities.StateAggregate;
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

        public City(Guid id, string name, Guid stateId)
        {
            Id = id;
            Name = name;
            StateId = stateId;
        }

        public bool IsInState(State state)
        {
            return StateId == state.Id;
        }
    }
}
