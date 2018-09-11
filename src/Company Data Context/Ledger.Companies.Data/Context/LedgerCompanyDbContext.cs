using Ledger.Companies.Data.EntityTypeConfiguration;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.CrossCutting.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Companies.Data.Context
{
    public class LedgerCompanyDbContext : DbContext, IDbContext<LedgerCompanyDbContext>
    {
        public DbSet<Company> Companies { get; set; }

        public LedgerCompanyDbContext(DbContextOptions<LedgerCompanyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}
