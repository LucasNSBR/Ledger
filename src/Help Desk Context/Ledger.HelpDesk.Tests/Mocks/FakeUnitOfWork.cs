using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.HelpDesk.Domain.Context;
using System;

namespace Ledger.HelpDesk.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork<ILedgerHelpDeskDbAbstraction>
    {
        public CommitResult Commit()
        {
            return CommitResult.Ok();
        }

        public void Rollback()
        {
        }
    }
}
