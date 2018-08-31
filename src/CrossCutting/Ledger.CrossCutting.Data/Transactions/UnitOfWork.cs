using Microsoft.EntityFrameworkCore;
using System;

namespace Ledger.CrossCutting.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CommitResult Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                return CommitResult.Ok();
            }
            catch(DbUpdateException dbex)
            {
                return CommitResult.Fail(dbex);
            }
            catch(InvalidOperationException ioex)
            {
                return CommitResult.Fail(ioex);
            }
            catch(NotSupportedException nsex)
            {
                return CommitResult.Fail(nsex);
            }
            catch(Exception ex)
            {
                return CommitResult.Fail(ex);
            }
        }

        public void Rollback()
        {
        }
    }
}
