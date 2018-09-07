using Microsoft.IdentityModel.Tokens;

namespace Ledger.CrossCutting.Identity.Abstractions
{
    public interface ISigningConfiguration
    {
        SecurityKey SecurityKey { get; }
        SigningCredentials GetSigningCredentials();
    }
}
