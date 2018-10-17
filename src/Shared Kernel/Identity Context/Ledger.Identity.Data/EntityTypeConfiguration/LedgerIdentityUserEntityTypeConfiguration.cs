using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Identity.Data.EntityTypeConfiguration
{
    public class LedgerIdentityUserEntityTypeConfiguration : IEntityTypeConfiguration<LedgerIdentityUser>
    {
        public void Configure(EntityTypeBuilder<LedgerIdentityUser> builder)
        {
            //Should be implemented later
        }
    }
}
