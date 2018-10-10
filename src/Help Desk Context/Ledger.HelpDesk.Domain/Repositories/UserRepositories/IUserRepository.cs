using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using System;

namespace Ledger.HelpDesk.Domain.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        User GetById(Guid id);
        void Register(User user);
    }
}
