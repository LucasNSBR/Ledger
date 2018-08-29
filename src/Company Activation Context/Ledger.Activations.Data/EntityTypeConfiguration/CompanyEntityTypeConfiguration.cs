using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Activations.Data.EntityTypeConfiguration
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .OwnsOne(o => o.Owner, cfg =>
                {
                    cfg.Property(o => o.Name).IsRequired().HasMaxLength(120);
                    cfg.Property(o => o.Age).IsRequired();
                    cfg.Property(o => o.Cpf).IsRequired().HasMaxLength(11);
                });
        }
    }
}
