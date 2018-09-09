using Microsoft.IdentityModel.Tokens;

namespace Ledger.Identity.Domain.Configuration.SigningConfigurations
{
    public interface ISigningConfiguration
    {
        SecurityKey SecurityKey { get; }
        SigningCredentials GetSigningCredentials();
    }
}
