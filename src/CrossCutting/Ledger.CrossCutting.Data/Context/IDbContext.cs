namespace Ledger.CrossCutting.Data.Context
{
    public interface IDbContext<T> 
    {
        int SaveChanges();
    }
}
