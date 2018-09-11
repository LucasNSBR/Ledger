using Ledger.CrossCutting.Data.Context;

namespace Ledger.Activations.Tests.Mocks
{
    public class FakeDbContext : IDbContext<FakeDbContext>
    {
        public int SaveChanges()
        {
            return 1;
        }
    }
}
