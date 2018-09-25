using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration
{
    public class TicketMessageEntityConfiguration : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(m => m.UserId)
                .IsRequired();

            builder
                .Property(m => m.MessageDate)
                .IsRequired();

            builder
                .Property(m => m.Body)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
