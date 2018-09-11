using System;

namespace Ledger.CrossCutting.Data.UnitOfWork
{
    public class CommitResult
    {
        public bool Success { get; }
        public Exception Exception { get; }

        //Default state is success
        private CommitResult()
        {
            Success = true;
        }

        private CommitResult(Exception exception)
        {
            Success = false;
            Exception = exception;
        }


        public static CommitResult Ok()
        {
            return new CommitResult();
        }

        public static CommitResult Fail(Exception exception)
        {
            return new CommitResult(exception);
        }
    }
}
