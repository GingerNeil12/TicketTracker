using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using TicketTracker.Infrastructure.Security.Interfaces;

namespace TicketTracker.Infrastructure.Security.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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

            return "";
        }
    }
}
