using Ledger.CrossCutting.Data.Context;

namespace Ledger.Activations.Data.Context
{
    public interface ILedgerActivationDbAbstraction : IDbContext<ILedgerActivationDbAbstraction>
    {
    }
}
