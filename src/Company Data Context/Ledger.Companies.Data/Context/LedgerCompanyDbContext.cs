using Ledger.Companies.Data.EntityTypeConfiguration;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Companies.Data.Context
{
    public class LedgerCompanyDbContext : DbContext, ILedgerCompanyDbAbstraction
    {
        public DbSet<Company> Companies { get; set; }

        public LedgerCompanyDbContext(DbContextOptions<LedgerCompanyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}
