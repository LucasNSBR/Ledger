using Ledger.CrossCutting.Data.Context;

namespace Ledger.Blog.Domain.Context
{
    public interface ILedgerBlogDbAbstraction : IDbContext<ILedgerBlogDbAbstraction>
    {
    }
}
