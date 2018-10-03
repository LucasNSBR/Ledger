using Ledger.Shared.Entities.StateAggregate;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.StateRepositories
{
    public interface IStateRepository
    {
        IQueryable<State> GetByName(string name);
        IQueryable<State> GetByCountry(Guid id);
        State GetById(Guid id);
    }
}
