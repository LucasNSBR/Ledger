using Ledger.Identity.Domain.Services.UserServices.UserResolver;
using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.Services;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationsController : BaseController
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [Route("countries")]
        public IActionResult GetAllCountries()
        {
            IQueryable<Country> countries = _locationService.GetAllCountries();

            return CreateResponse(countries);
        }

        [HttpGet]
        [Route("countries/{id}/states")]
        public IActionResult GetStatesByCountry(Guid id)
        {
            IQueryable<State> states = _locationService.GetStatesByCountry(id);

            return CreateResponse(states);
        }

        [HttpGet]
        [Route("states/{id}/cities")]
        public IActionResult GetCitiesByState(Guid id)
        {
            IQueryable<City> cities = _locationService.GetCitiesByState(id);

            return CreateResponse(cities);
        }
    }
}