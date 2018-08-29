using Ledger.Activations.Data.EntityTypeConfiguration;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Activations.Data.Context
{
    public class LedgerActivationDbContext : DbContext
    {
        public DbSet<Activation> Activations { get; set; }

        public LedgerActivationDbContext(DbContextOptions<LedgerActivationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ActivationEntityTypeConfiguration());
            builder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}
