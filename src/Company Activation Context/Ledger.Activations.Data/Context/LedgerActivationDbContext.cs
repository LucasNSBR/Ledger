using Ledger.Activations.Data.EntityTypeConfiguration;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Activations.Data.Context
{
    public class LedgerActivationDbContext : DbContext, ILedgerActivationDbAbstraction
    {
        public DbSet<Activation> Activations { get; set; }
        public DbSet<Company> Companies { get; set; }

        public LedgerActivationDbContext(DbContextOptions<LedgerActivationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}
