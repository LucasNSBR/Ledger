using Ledger.Companies.Data.Context;
using Ledger.CrossCutting.Data.UnitOfWork;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork<FakeDbContext>
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
