using Ledger.Shared.Entities.Locations;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.CountryRepositories
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAllCountries();
        IQueryable<Country> GetByName(string name);
        Country GetById(Guid id);
    }
}
