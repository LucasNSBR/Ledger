using Ledger.Shared.Entities.Locations;
using System;
using System.Linq;

namespace Ledger.Shared.Locations.Repositories.StateRepositories
{
    public interface IStateRepository
    {
        IQueryable<State> GetAllStates();
        State GetById(Guid id);
    }
}
