using Ledger.Identity.Domain.Configuration.SigningConfigurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ledger.Identity.Domain.Services.SigningServices
{
    public class SigningService : ISigningService
    {
        private readonly byte[] SALT_KEY;

        private SecurityKey _securityKey;
        public SecurityKey SecurityKey
        {
            get
            {
                if(_securityKey == null)
                    _securityKey = new SymmetricSecurityKey(SALT_KEY);

                return _securityKey;
            }
        }

        public SigningService(IOptions<SigningOptions> options)
        {
            SALT_KEY = Encoding.UTF8.GetBytes(options.Value.SALT_KEY);
        }

        public SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials(
                SecurityKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
