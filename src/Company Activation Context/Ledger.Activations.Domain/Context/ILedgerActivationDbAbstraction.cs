using Ledger.CrossCutting.Data.Context;

namespace Ledger.Activations.Domain.Context
{
    public interface ILedgerActivationDbAbstraction : IDbContext<ILedgerActivationDbAbstraction>
    {
    }
}
