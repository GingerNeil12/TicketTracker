using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Application.Models.Security;
using TicketTracker.Infrastructure.Security.Models;
using TicketTracker.Shared.General.Security;
using TicketTracker.Shared.ViewModels.Security;

namespace TicketTracker.Infrastructure.Security.Services
{
    public class Authenticator : IAuthenticator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public async Task<AuthenticationResult> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if(user == null)
            {
                return new AuthenticationResult
                (
                    AuthenticationCode.EmailOrPasswordIncorrect,
                    ErrorMessages.EmailOrPasswordIncorrect
                );
            }

            if(await _userManager.IsLockedOutAsync(user))
            {
                return new AuthenticationResult
                (
                    AuthenticationCode.Success,
                    ErrorMessages.AccountLocked
                );
            }

            if(!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthenticationResult
                (
                    AuthenticationCode.EmailOrPasswordIncorrect,
                    ErrorMessages.EmailOrPasswordIncorrect
                );
            }

            // Update user with new RefreshToken etc

            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Claim>> GetClaimsFromUserAsync(ApplicationUser user)
        {
            var result = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(TokenClaims.RefreshToken, user.RefreshToken)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                result.Add(new Claim(ClaimTypes.Role, role));
            }
            
            return result;
        }

        private static class ErrorMessages
        {
            public const string EmailOrPasswordIncorrect = "Email or Password is incorrect.";
            public const string AccountLocked = "Account locked.";
        }
    }
}
