using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.HelpDesk.Data.EntityTypeConfiguration.TicketTypeConfiguration
{
    public class TicketConversationEntityTypeConfiguration : IEntityTypeConfiguration<TicketConversation>
    {
        public void Configure(EntityTypeBuilder<TicketConversation> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasMany(c => c.Messages)
                .WithOne()
                .HasForeignKey(tm => tm.ConversationId);
        }
    }
}
