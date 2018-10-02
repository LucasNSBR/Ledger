using Ledger.Shared.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Shared.Locations.EntityTypeConfiguration
{
    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(s => s.Initials)
                .IsRequired()
                .HasMaxLength(2);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
