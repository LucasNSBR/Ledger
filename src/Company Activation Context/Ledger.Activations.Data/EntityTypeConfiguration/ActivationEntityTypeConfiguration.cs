using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Activations.Data.EntityTypeConfiguration
{
    public class ActivationEntityTypeConfiguration : IEntityTypeConfiguration<Activation>
    {
        public void Configure(EntityTypeBuilder<Activation> builder)
        {
            builder
                .HasKey(k => k.Id);

            //Configure PK to be FK also
            builder
                .HasOne(a => a.Company)
                .WithOne()
                .HasForeignKey<Activation>(a => a.Id);

            builder
                .Property(a => a.Status)
                .IsRequired();

            builder
                .Property(a => a.TenantId)
                .IsRequired();
        }
    }
}
