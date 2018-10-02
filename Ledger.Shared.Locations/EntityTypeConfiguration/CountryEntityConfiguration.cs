using Ledger.Shared.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Shared.Locations.EntityTypeConfiguration
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(c => c.ShortName)
                .IsRequired()
                .HasMaxLength(4);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
