using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Notifications;
using System.Collections.Generic;

namespace Ledger.Shared.Locations.Services
{
    public class LocationResult
    {
        public bool Success { get; }

        public City City { get; private set; }
        public State State { get; private set; }
        public Country Country { get; private set; }

        private readonly List<DomainNotification> _notifications;

        public IReadOnlyList<DomainNotification> Notifications
        {
            get
            {
                return _notifications;
            }
        }

        private LocationResult()
        {
            Success = true;
            _notifications = new List<DomainNotification>();
        }

        private LocationResult(List<DomainNotification> notifications)
        {
            Success = false;
            _notifications = notifications;
        }

        public static LocationResult Ok()
        {
            return new LocationResult();
        }

        public static LocationResult Ok(City city, State state, Country country)
        {
            return new LocationResult
            {
                City = city,
                State = state,
                Country = country,
            };
        }

        public static LocationResult Failure(params DomainNotification[] notifications)
        {
            List<DomainNotification> list = new List<DomainNotification>();
            
            foreach (DomainNotification notification in notifications)
            {
                list.Add(notification);
            }

            return new LocationResult(list);
        }
    }
}
