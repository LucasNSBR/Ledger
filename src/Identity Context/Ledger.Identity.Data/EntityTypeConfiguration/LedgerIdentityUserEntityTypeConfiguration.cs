using Ledger.Identity.Domain.Models.Aggregates.UserAggregate.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Identity.Data.EntityTypeConfiguration
{
    public class LedgerIdentityUserEntityTypeConfiguration : IEntityTypeConfiguration<LedgerIdentityUser>
    {
        public void Configure(EntityTypeBuilder<LedgerIdentityUser> builder)
        {
        }
    }
}
