using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration
{
    public class TicketCategoryEntityTypeConfiguration : IEntityTypeConfiguration<TicketCategory>
    {
        public void Configure(EntityTypeBuilder<TicketCategory> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
