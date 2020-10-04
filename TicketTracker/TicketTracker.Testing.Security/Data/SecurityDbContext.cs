using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketTracker.Infrastructure.Security.Models;

namespace TicketTracker.Testing.Security.Data
{
    internal class SecurityDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public SecurityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
