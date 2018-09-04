using Ledger.CrossCutting.Identity.EntityTypeConfiguration;
using Ledger.CrossCutting.Identity.Models.Roles;
using Ledger.CrossCutting.Identity.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ledger.CrossCutting.Identity.Context
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
