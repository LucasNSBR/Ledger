using Ledger.CrossCutting.Data.Context;

namespace Ledger.CrossCutting.Data.UnitOfWork
{
    public interface IUnitOfWork<TDbContext> 
        where TDbContext : IDbContext<TDbContext>
    {
        CommitResult Commit();
        void Rollback();
    }
}