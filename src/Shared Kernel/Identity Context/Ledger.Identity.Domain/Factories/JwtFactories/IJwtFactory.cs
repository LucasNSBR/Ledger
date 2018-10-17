using System.Security.Claims;

namespace Ledger.Identity.Domain.Services
{
    public interface IJwtFactory
    {
        string WriteToken(ClaimsIdentity claimsIdentity);
    }
}
