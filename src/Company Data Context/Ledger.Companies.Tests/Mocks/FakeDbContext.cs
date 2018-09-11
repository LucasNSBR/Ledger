using Ledger.Companies.Data.Context;
using Ledger.CrossCutting.Data.Context;

namespace Ledger.Companies.Tests.Mocks
{
    public class FakeDbContext : IDbContext<FakeDbContext>
    {
        public int SaveChanges()
        {
            return 1;
        }
    }
}
