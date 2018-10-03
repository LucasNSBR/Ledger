using System;

namespace Ledger.Shared.Locations.Services
{
    public interface ILocationService
    {
        LocationResult TryGetLocation(Guid cityId, Guid stateId, Guid countryId);
    }
}
