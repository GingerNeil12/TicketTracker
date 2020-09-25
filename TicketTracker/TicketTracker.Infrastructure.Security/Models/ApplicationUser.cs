using System;
using Microsoft.AspNetCore.Identity;

namespace TicketTracker.Infrastructure.Security.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
