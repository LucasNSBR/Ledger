using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Shared.Locations.EntityTypeConfiguration.StateEntityConfiguraiton
{
    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(s => s.ShortName)
                .IsRequired()
                .HasMaxLength(2);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(c => c.CountryId);
        }
    }
}
