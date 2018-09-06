using Ledger.CrossCutting.Identity.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Ledger.CrossCutting.Identity.Models.Services
{
    public class LedgerUserManager : UserManager<LedgerIdentityUser>
    {
        public LedgerUserManager(IUserStore<LedgerIdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<LedgerIdentityUser> passwordHasher, IEnumerable<IUserValidator<LedgerIdentityUser>> userValidators, IEnumerable<IPasswordValidator<LedgerIdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<LedgerIdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
