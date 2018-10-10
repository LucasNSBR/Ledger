using Ledger.HelpDesk.Data.EntityTypeConfiguration.CategoryTypeConfiguration;
using Ledger.HelpDesk.Data.EntityTypeConfiguration.TicketTypeConfiguration;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.HelpDesk.Data.Context
{
    public class LedgerHelpDeskDbContext : DbContext, ILedgerHelpDeskDbAbstraction
    {
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public LedgerHelpDeskDbContext(DbContextOptions<LedgerHelpDeskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TicketCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConversationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketMessageEntityConfiguration());
        }
    }
}
