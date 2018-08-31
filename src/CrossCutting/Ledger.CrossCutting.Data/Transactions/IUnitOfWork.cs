namespace Ledger.CrossCutting.Data.Transactions
{
    public interface IUnitOfWork
    {
        bool Commit();
        void Rollback();
    }
}
