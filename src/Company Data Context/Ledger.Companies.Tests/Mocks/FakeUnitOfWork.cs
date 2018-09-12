using Ledger.Companies.Data.Context;
using Ledger.Companies.Domain.Context;
using Ledger.CrossCutting.Data.UnitOfWork;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork<ILedgerCompanyDbAbstraction>
    {
        public CommitResult Commit()
        {
            return CommitResult.Ok();
        }

        public void Rollback()
        {
        }
    }
}
