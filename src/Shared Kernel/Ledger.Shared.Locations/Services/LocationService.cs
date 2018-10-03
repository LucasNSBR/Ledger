using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.Repositories.CityRepositories;
using Ledger.Shared.Locations.Repositories.CountryRepositories;
using Ledger.Shared.Locations.Repositories.StateRepositories;
using Ledger.Shared.Notifications;
using System;

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
        
        public LocationResult TryGetLocation(Guid cityId, Guid stateId, Guid countryId)
        {
            City city = _cityRepository.GetById(cityId);
            State state = _stateRepository.GetById(stateId);
            Country country = _countryRepository.GetById(countryId);

            if (city == null || state == null || country == null)
                return LocationResult.Failure(
                    new DomainNotification("Id Inválido", "Não foi possível encontrar a localização pelo Id."));

            if (!city.IsInState(state) || !state.IsInCountry(country))
                return LocationResult.Failure(
                    new DomainNotification("Localização inválida", "A cidade e/ou estado não pertence à nação."));

            return LocationResult.Ok(city, state, country);
        }
    }
}
