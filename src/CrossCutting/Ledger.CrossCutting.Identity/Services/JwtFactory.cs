using Ledger.CrossCutting.Identity.Abstractions;
using Ledger.CrossCutting.Identity.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Ledger.CrossCutting.Identity.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IOptions<JwtTokenOptions> _options;
        private readonly ISigningConfiguration _signingConfiguration;

        public JwtFactory(IOptions<JwtTokenOptions> options, ISigningConfiguration signingConfiguration)
        {
            _options = options;
            _signingConfiguration = signingConfiguration;
        }

        public string WriteToken(string name, IEnumerable<Claim> claims)
        {
            GenericIdentity genericIdentity = new GenericIdentity(name);
            ClaimsIdentity identity = new ClaimsIdentity(genericIdentity, claims);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _options.Value.Audience,
                Expires = DateTime.Now.AddSeconds(_options.Value.ExpiresInSeconds),
                Issuer = _options.Value.Issuer,
                IssuedAt = _options.Value.IssuedAt,
                NotBefore = _options.Value.NotBefore,
                Subject = identity,                
                SigningCredentials = _signingConfiguration.GetSigningCredentials(),
            });

            return handler.WriteToken(token);
        }
    }
}
