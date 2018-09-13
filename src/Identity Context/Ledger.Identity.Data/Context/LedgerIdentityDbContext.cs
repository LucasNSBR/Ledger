using Ledger.Identity.Data.EntityTypeConfiguration;
using Ledger.Identity.Domain.Models.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ledger.Identity.Data.Context
{
    public class LedgerIdentityDbContext : IdentityDbContext<LedgerIdentityUser, LedgerIdentityRole, Guid>
    {
        public LedgerIdentityDbContext(DbContextOptions<LedgerIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LedgerIdentityUserEntityTypeConfiguration());
        }
    }
}
