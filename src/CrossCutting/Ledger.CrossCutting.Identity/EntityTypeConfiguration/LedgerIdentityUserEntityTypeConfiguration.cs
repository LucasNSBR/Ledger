using Ledger.CrossCutting.Identity.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.CrossCutting.Identity.EntityTypeConfiguration
{
    public class LedgerIdentityUserEntityTypeConfiguration : IEntityTypeConfiguration<LedgerIdentityUser>
    {
        public void Configure(EntityTypeBuilder<LedgerIdentityUser> builder)
        {
        }
    }
}
