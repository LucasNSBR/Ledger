using Ledger.CrossCutting.Data.Context;

namespace Ledger.Companies.Data.Context
{
    public interface ILedgerCompanyDbAbstraction : IDbContext<ILedgerCompanyDbAbstraction>
    {
    }
}
