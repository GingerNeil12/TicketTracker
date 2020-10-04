using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TicketTracker.Infrastructure.Security.Models;

namespace TicketTracker.Testing.Security.Data
{
    internal class SecurityDbContext
        : IdentityDbContext<ApplicationUser>
    {
    }
}
