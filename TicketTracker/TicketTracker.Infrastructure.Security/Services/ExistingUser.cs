using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketTracker.Application.Interfaces.Common;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Infrastructure.Security.Models;

namespace TicketTracker.Infrastructure.Security.Services
{
    public class ExistingUser : IExistingUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentDateTime _currentDateTime;

        public ExistingUser
        (
            UserManager<ApplicationUser> userManager,
            ICurrentDateTime currentDateTime
        )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _currentDateTime = currentDateTime ?? throw new ArgumentNullException(nameof(currentDateTime));
        }

        public async Task AddRefreshTokenToUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiry = _currentDateTime.Now.AddMinutes(30);
            await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<Claim>> GetAccessClaimsForUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("RefreshToken", user.RefreshToken)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                result.Add(new Claim(ClaimTypes.Role, role));
            }

            return result;
        }

        private static string GenerateRefreshToken()
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[32];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
