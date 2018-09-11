using Ledger.CrossCutting.Data.Context;

namespace Ledger.CrossCutting.Data.UnitOfWork
{
    public interface IUnitOfWork<T> where T : IDbContext<T>
    {
        CommitResult Commit();
        void Rollback();
    }
}
