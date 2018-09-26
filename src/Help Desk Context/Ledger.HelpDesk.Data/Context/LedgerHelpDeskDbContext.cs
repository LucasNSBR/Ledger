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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TicketCategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new TicketEntityTypeConfiguration());
            builder.ApplyConfiguration(new TicketMessageEntityConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
        }
    }
}
