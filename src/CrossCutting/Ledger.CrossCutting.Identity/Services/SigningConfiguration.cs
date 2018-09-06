using Ledger.CrossCutting.Identity.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ledger.CrossCutting.Identity.Services
{
    public class SigningConfiguration : ISigningConfiguration
    {
        private readonly IConfiguration _configuration; 

        public SigningConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SALT_KEY"])), SecurityAlgorithms.HmacSha256);
        }
    }
}
