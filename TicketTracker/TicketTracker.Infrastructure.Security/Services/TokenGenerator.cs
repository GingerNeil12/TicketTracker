using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TicketTracker.Application.Interfaces.Common;
using TicketTracker.Infrastructure.Security.Interfaces;

namespace TicketTracker.Infrastructure.Security.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentDateTime _currentDateTime;

        public TokenGenerator
        (
            IConfiguration configuration,
            ICurrentDateTime currentDateTime
        )
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _currentDateTime = currentDateTime ?? throw new ArgumentNullException(nameof(currentDateTime));
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            if (!claims.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(claims));
            }

            var securityKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var credentials = new SigningCredentials
            (
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var header = new JwtHeader(credentials);
            
            var payload = new JwtPayload
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims,
                notBefore: _currentDateTime.Now,
                expires: _currentDateTime.Now.AddMinutes(15),
                issuedAt: _currentDateTime.Now
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
