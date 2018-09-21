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
                .OwnsOne(c => c.ContratoSocialPicture);

            builder
                .OwnsOne(c => c.AlteracaoContratoSocialPicture);

            builder
                .OwnsOne(c => c.OwnerDocumentPicture);

            builder
                .OwnsOne(c => c.ExtraDocumentPicture);
        }
    }
}
