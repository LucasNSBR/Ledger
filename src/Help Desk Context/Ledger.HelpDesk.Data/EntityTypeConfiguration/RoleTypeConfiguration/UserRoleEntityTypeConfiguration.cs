using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration.RoleTypeConfiguration
{
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(k =>
                new
                {
                    k.RoleId,
                    k.UserId
                });

            builder
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(r => r.RoleId);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId);
        }
    }
}
