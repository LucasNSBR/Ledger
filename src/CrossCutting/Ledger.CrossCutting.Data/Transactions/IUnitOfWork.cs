namespace Ledger.CrossCutting.Data.Transactions
{
    public interface IUnitOfWork
    {
        CommitResult Commit();
        void Rollback();
    }
}
