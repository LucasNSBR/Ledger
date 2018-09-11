using Ledger.Activations.Data.Context;
using Ledger.CrossCutting.Data.UnitOfWork;

namespace Ledger.Activations.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork<ILedgerActivationDbAbstraction>
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
