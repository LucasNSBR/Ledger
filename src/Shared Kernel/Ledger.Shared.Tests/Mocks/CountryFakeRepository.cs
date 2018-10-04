using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Locations.Repositories.CountryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Tests.Mocks
{
    public class CountryFakeRepository : ICountryRepository
    {
        private readonly List<Country> _countries;

        public CountryFakeRepository()
        {
            _countries = new List<Country>();

            _countries.Add(new Country(new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932"), "BR", "Brazil"));
            _countries.Add(new Country(new Guid("43231eb6-2aab-463c-9286-93827dd0eb17"), "USA", "United States of America"));
        }

        public IQueryable<Country> GetAllCountries()
        {
            return _countries.AsQueryable();
        }

        public Country GetById(Guid id)
        {
            return _countries.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Country> GetByName(string name)
        {
            return _countries.Where(c => c.Name.ToLower().Contains(name)).AsQueryable();
        }
    }
}
