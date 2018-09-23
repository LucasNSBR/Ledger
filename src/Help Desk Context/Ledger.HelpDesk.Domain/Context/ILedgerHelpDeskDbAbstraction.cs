using Ledger.CrossCutting.Data.Context;

namespace Ledger.HelpDesk.Domain.Context
{
    public interface ILedgerHelpDeskDbAbstraction : IDbContext<ILedgerHelpDeskDbAbstraction>
    {
    }
}
