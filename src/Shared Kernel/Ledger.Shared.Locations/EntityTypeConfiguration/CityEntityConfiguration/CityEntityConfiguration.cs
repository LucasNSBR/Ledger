using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Shared.Locations.EntityTypeConfiguration.CityEntityConfiguration
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
                .HasOne<State>()
                .WithMany()
                .HasForeignKey(c => c.StateId);
        }
    }
}
