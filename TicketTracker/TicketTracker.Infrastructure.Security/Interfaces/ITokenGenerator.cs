using System.Collections.Generic;
using System.Security.Claims;

namespace TicketTracker.Infrastructure.Security.Interfaces
{
    public interface ITokenGenerator
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}
