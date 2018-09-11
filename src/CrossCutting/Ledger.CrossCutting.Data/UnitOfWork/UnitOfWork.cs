using Ledger.CrossCutting.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ledger.CrossCutting.Data.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> 
                            where T : IDbContext<T>
    {
        private readonly IDbContext<T> _dbContext;

        public UnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual CommitResult Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                return CommitResult.Ok();
            }
            catch (DbUpdateException dbex)
            {
                return CommitResult.Fail(dbex);
            }
            catch (InvalidOperationException ioex)
            {
                return CommitResult.Fail(ioex);
            }
            catch (NotSupportedException nsex)
            {
                return CommitResult.Fail(nsex);
            }
            catch (Exception ex)
            {
                return CommitResult.Fail(ex);
            }
        }

        public void Rollback()
        {
        }
    }
}
