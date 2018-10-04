using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.EntityTypeConfiguration.CityEntityConfiguration;
using Ledger.Shared.Locations.EntityTypeConfiguration.CountryEntityConfiguration;
using Ledger.Shared.Locations.EntityTypeConfiguration.StateEntityConfiguraiton;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Shared.Locations.Context
{
    public class LedgerLocationDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }

        public LedgerLocationDbContext(DbContextOptions<LedgerLocationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StateEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
        }
    }
}
