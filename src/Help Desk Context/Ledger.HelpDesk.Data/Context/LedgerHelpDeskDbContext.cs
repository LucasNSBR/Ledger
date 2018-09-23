using Ledger.HelpDesk.Data.EntityTypeConfiguration;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.HelpDesk.Data.Context
{
    public class LedgerHelpDeskDbContext : DbContext, ILedgerHelpDeskDbAbstraction
    {
        public DbSet<TicketCategory> TicketCategories { get; set; }

        public LedgerHelpDeskDbContext(DbContextOptions<LedgerHelpDeskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TicketCategoryEntityTypeConfiguration());
        }
    }
}
