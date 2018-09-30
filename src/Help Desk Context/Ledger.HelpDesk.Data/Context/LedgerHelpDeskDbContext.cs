using Ledger.HelpDesk.Data.EntityTypeConfiguration.CategoryTypeConfiguration;
using Ledger.HelpDesk.Data.EntityTypeConfiguration.RoleTypeConfiguration;
using Ledger.HelpDesk.Data.EntityTypeConfiguration.TicketTypeConfiguration;
using Ledger.HelpDesk.Data.EntityTypeConfiguration.UserTypeConfiguration;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.HelpDesk.Data.Context
{
    public class LedgerHelpDeskDbContext : DbContext, ILedgerHelpDeskDbAbstraction
    {
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public LedgerHelpDeskDbContext(DbContextOptions<LedgerHelpDeskDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TicketCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConversationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketMessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
        }
    }
}
