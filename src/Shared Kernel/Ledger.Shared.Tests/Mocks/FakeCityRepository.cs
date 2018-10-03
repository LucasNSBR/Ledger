using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Locations.Repositories.CityRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Tests.Mocks
{
    public class FakeCityRepository : ICityRepository
    {
        private List<City> _cities;

        public FakeCityRepository()
        {
            _cities = new List<City>();

            _cities.Add(new City(new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4"), "Belo Horizonte", new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3")));
        }

        public City GetById(Guid id)
        {
            return _cities.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<City> GetByName(string name)
        {
            return _cities.Where(c => c.Name.ToLower().Contains(name.ToLower())).AsQueryable();
        }

        public IQueryable<City> GetByState(Guid stateId)
        {
            return _cities.Where(c => c.StateId == stateId).AsQueryable();
        }
    }
}
