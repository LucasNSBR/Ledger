using Microsoft.IdentityModel.Tokens;

namespace Ledger.Identity.Domain.Services.SigningServices
{
    public interface ISigningService
    {
        SecurityKey SecurityKey { get; }
        SigningCredentials GetSigningCredentials();
    }
}
