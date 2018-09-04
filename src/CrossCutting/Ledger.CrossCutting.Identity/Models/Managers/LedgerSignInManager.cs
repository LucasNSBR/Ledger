using Ledger.CrossCutting.Identity.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ledger.CrossCutting.Identity.Models.Managers
{
    public class LedgerSignInManager : SignInManager<LedgerIdentityUser>
    {
        public LedgerSignInManager(UserManager<LedgerIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<LedgerIdentityUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<LedgerIdentityUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}
