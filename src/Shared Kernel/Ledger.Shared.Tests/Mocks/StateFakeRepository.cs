using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.Repositories.StateRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Tests.Mocks
{
    public class StateFakeRepository : IStateRepository
    {
        private List<State> _states;

        public StateFakeRepository()
        {
            _states = new List<State>();

            _states.Add(new State(new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3"), "", "", new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932")));
        }

        public IQueryable<State> GetByCountry(Guid id)
        {
            return _states.Where(s => s.CountryId == id).AsQueryable();
        }

        public State GetById(Guid id)
        {
            return _states.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<State> GetByName(string name)
        {
            return _states.Where(s => s.Name.ToLower().Contains(name.ToLower())).AsQueryable();
        }
    }
}
