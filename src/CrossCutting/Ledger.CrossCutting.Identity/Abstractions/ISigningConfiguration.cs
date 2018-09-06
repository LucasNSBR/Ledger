using Microsoft.IdentityModel.Tokens;

namespace Ledger.CrossCutting.Identity.Abstractions
{
    public interface ISigningConfiguration
    {
        SigningCredentials GetSigningCredentials();
    }
}
