using System.Collections.Generic;
using System.Security.Claims;

namespace Ledger.CrossCutting.Identity.Abstractions
{
    public interface IJwtFactory
    {
        string WriteToken(string name, IEnumerable<Claim> claims);
    }
}
