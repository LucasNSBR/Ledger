namespace Ledger.CrossCutting.Data.Context
{
    public interface IDbContext<TDbContext> 
            where TDbContext : IDbContext<TDbContext>
    {
        int SaveChanges();
    }
}
