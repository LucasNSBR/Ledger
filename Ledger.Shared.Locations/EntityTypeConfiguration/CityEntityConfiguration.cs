using Ledger.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Shared.Locations.EntityTypeConfiguration
{
    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .HasOne(c => c.State)
                .WithMany()
                .HasForeignKey(c => c.StateId);
        }
    }
}
