using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.Repositories.CityRepositories;
using Ledger.Shared.Locations.Repositories.CountryRepositories;
using Ledger.Shared.Locations.Repositories.StateRepositories;
using Ledger.Shared.Notifications;
using System;
using System.Linq;

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

        public IQueryable<City> GetCitiesByState(Guid stateId)
        {
            return _cityRepository.GetByState(stateId);
        }

        public IQueryable<State> GetStatesByCountry(Guid countryId)
        {
            return _stateRepository.GetByCountry(countryId);
        }

        public IQueryable<Country> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }

        public LocationResult TryGetLocation(Guid cityId, Guid stateId, Guid countryId)
        {
            City city = _cityRepository.GetById(cityId);
            State state = _stateRepository.GetById(stateId);
            Country country = _countryRepository.GetById(countryId);

            if (city == null || state == null || country == null)
                return LocationResult.Failure(
                    new DomainNotification("Id Inválido", "Não foi possível encontrar a localização pelo Id."));

            if (!city.IsInState(state))
                return LocationResult.Failure(
                    new DomainNotification("Cidade inválida", "A cidade especificada não pertence ao estado."));

            if (!state.IsInCountry(country))
                return LocationResult.Failure(
                    new DomainNotification("Estado inválido", "O estado especificado não pertence à nação."));

            return LocationResult.Ok(city, state, country);
        }
    }
}
