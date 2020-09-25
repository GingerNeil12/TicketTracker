using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly IExistingUser _existingUser;

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

            if(await UserIsLockedOutAsync(user))
            {
                return new AuthenticationResult
                (
                    AuthenticationCode.Success,
                    ErrorMessages.AccountLocked
                );
            }

            if(await UserPasswordIncorrectAsync(user, loginDto.Password))
            {
                return new AuthenticationResult
                (
                    AuthenticationCode.EmailOrPasswordIncorrect,
                    ErrorMessages.EmailOrPasswordIncorrect
                );
            }

            await _existingUser.AddRefreshTokenToUserAsync(user.Id);

            throw new NotImplementedException();
        }

        private Task<bool> UserIsLockedOutAsync(ApplicationUser user)
        {
            return _userManager.IsLockedOutAsync(user);
        }

        private async Task<bool> UserPasswordIncorrectAsync
        (
            ApplicationUser user,
            string password
        )
        {
            return !await _userManager.CheckPasswordAsync(user, password);
        }

        private static class ErrorMessages
        {
            public const string EmailOrPasswordIncorrect = "Email or Password is incorrect.";
            public const string AccountLocked = "Account locked.";
        }
    }
}
