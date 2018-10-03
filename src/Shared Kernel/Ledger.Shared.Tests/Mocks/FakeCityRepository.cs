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
            _cities.Add(new City(new Guid("172c77e9-1ef1-4673-85f2-307917807115"), "São Paulo", new Guid("cc66a0e0-7eff-4e0b-8b14-27a195de3257")));
            _cities.Add(new City(new Guid("baa3b30e-8da9-4f3e-a0c3-fae93da05346"), "Redmond", new Guid("066ed615-e330-4e85-a79c-68e498b3d363")));
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
