using Ledger.Identity.Domain.Models.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ledger.Identity.Domain.Models.Services.UserServices
{
    public class LedgerSignInManager : SignInManager<LedgerIdentityUser>
    {
        public LedgerSignInManager(UserManager<LedgerIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<LedgerIdentityUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<LedgerIdentityUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}
