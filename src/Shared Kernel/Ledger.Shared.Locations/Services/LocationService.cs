using Ledger.Shared.Locations.Repositories.CityRepositories;
using Ledger.Shared.Locations.Repositories.CountryRepositories;
using Ledger.Shared.Locations.Repositories.StateRepositories;

namespace Ledger.Shared.Locations.Services
{
    public class LocationService : ILocationService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;

        public LocationService(ICityRepository cityRepository, IStateRepository stateRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _countryRepository = countryRepository;
        }
    }
}
