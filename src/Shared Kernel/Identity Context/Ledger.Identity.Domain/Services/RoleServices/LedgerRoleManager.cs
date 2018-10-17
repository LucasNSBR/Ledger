using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Ledger.Identity.Domain.Services.RoleServices
{
    public class LedgerRoleManager : RoleManager<LedgerIdentityRole>
    {
        public LedgerRoleManager(IRoleStore<LedgerIdentityRole> store, IEnumerable<IRoleValidator<LedgerIdentityRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<LedgerIdentityRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
