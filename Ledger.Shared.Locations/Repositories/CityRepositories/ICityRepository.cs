using Ledger.Shared.Entities;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.CityRepositories
{
    public interface ICityRepository
    {
        IQueryable<City> GetByName(string name);
        IQueryable<City> GetByState(Guid stateId);
        City GetById(Guid id);
    }
}
