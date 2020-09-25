using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicketTracker.Application.Interfaces.Security
{
    public interface IExistingUser
    {
        Task AddRefreshTokenToUserAsync(string userId);
        Task<IEnumerable<Claim>> GetAccessClaimsForUserAsync(string userId);
    }
}
