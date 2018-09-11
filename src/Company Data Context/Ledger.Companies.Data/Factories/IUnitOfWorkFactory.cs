using Ledger.Companies.Data.Context;
using Ledger.CrossCutting.Data.UnitOfWork;

namespace Ledger.Companies.Data.Factories
{
    public interface IUnitOfWorkFactory : IUnitOfWork<LedgerCompanyDbContext>
    {
    }
}
