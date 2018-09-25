using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration
{
    public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .OwnsOne(t => t.TicketStatus, cfg =>
                {
                    cfg.Property(s => s.DateOpened).IsRequired();
                });

            builder
                .Property(t => t.CategoryId)
                .IsRequired();

            builder
                .HasOne(t => t.Conversation)
                .WithOne()
                .HasForeignKey<TicketConversation>(tc => tc.Id);

            builder
                .Property(p => p.TicketUserId)
                .IsRequired();

            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(t => t.Details)
                .IsRequired()
                .HasMaxLength(2000);

            builder
                .OwnsOne(t => t.IssuePicture);
        }
    }
}
