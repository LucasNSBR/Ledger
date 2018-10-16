using Ledger.Blog.Domain.Context;
using Ledger.CrossCutting.Data.UnitOfWork;

namespace Ledger.Blog.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork<ILedgerBlogDbAbstraction>
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
