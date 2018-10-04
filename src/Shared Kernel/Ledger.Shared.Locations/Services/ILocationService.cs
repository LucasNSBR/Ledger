using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Services
{
    public interface ILocationService
    {
        IQueryable<City> GetCitiesByState(Guid stateId);
        IQueryable<State> GetStatesByCountry(Guid countryId);
        IQueryable<Country> GetAllCountries();
        LocationResult TryGetLocation(Guid cityId, Guid stateId, Guid countryId);
    }
}
