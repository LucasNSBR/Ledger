using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Abstractions
{
    public interface IJwtFactory
    {
        string WriteToken(ClaimsIdentity claimsIdentity);
    }
}
