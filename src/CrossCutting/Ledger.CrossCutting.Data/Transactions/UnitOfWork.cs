using Microsoft.EntityFrameworkCore;

namespace Ledger.CrossCutting.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Rollback()
        {
        }
    }
}
