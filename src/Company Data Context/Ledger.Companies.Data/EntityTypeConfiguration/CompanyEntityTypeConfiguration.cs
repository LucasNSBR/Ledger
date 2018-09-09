using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Companies.Data.EntityTypeConfiguration
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder
                .OwnsOne(c => c.Email, cfg =>
                {
                    cfg.Property(email => email.Email).IsRequired().HasMaxLength(150);
                });

            builder
                .OwnsOne(c => c.Phone, cfg =>
                {
                    cfg.Property(phone => phone.Number).IsRequired().HasMaxLength(15);
                });

            builder
                .OwnsOne(c => c.Cnpj, cfg =>
                {
                    cfg.Property(cnpj => cnpj.Number).IsRequired().HasMaxLength(14);
                });

            builder
                .OwnsOne(c => c.InscricaoEstadual, cfg =>
                {
                    cfg.Property(inscrEstadual => inscrEstadual.Number).IsRequired().HasMaxLength(16);
                });

            builder
                .OwnsOne(c => c.Address, cfg =>
                {
                    cfg.Property(address => address.Street).IsRequired().HasMaxLength(250);
                    cfg.Property(address => address.Neighborhood).IsRequired().HasMaxLength(100);
                    cfg.Property(address => address.Complementation).HasMaxLength(250);
                    cfg.Property(address => address.City).IsRequired().HasMaxLength(100);
                    cfg.Property(address => address.State).IsRequired().HasMaxLength(100);
                });
        }
    }
}
