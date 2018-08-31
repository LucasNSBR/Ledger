using Ledger.CrossCutting.Data.Transactions;

namespace Ledger.Activations.Tests.Mocks
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
