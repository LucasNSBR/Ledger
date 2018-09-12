using Ledger.CrossCutting.Data.Context;

namespace Ledger.Companies.Domain.Context
{
    public interface ILedgerCompanyDbAbstraction : IDbContext<ILedgerCompanyDbAbstraction>
    {
    }
}
