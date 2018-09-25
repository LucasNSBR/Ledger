using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
