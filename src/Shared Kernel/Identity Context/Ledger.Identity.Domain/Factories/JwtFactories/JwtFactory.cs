using Ledger.Identity.Domain.Configuration.JwtConfigurations;
using Ledger.Identity.Domain.Services.SigningServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ledger.Identity.Domain.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IOptions<JwtTokenOptions> _options;
        private readonly ISigningService _signingConfiguration;

        public JwtFactory(IOptions<JwtTokenOptions> options, ISigningService signingConfiguration)
        {
            _options = options;
            _signingConfiguration = signingConfiguration;
        }

        public string WriteToken(ClaimsIdentity claimsIdentity)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _options.Value.Audience,
                Expires = DateTime.Now.AddSeconds(_options.Value.ExpiresInSeconds),
                Issuer = _options.Value.Issuer,
                IssuedAt = _options.Value.IssuedAt,
                NotBefore = _options.Value.NotBefore,
                Subject = claimsIdentity,
                SigningCredentials = _signingConfiguration.GetSigningCredentials(),
            });

            return handler.WriteToken(token);
        }
    }
}
