using Ledger.CrossCutting.Data.Transactions;

namespace Ledger.Companies.Domain.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork
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
