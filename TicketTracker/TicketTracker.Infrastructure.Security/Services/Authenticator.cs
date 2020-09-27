using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Application.Models.Security;
using TicketTracker.Infrastructure.Security.Interfaces;
using TicketTracker.Infrastructure.Security.Models;
using TicketTracker.Shared.ViewModels.Security;

namespace TicketTracker.Infrastructure.Security.Services
{
    public class Authenticator : IAuthenticator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IExistingUser _existingUser;
        private readonly ITokenGenerator _tokenGenerator;

        public Authenticator
        (
            UserManager<ApplicationUser> userManager,
            IExistingUser existingUser,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _existingUser = existingUser ?? throw new ArgumentNullException(nameof(existingUser));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<AuthenticationResult> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if (user == null)
            {
                return new AuthenticationResult
                (
                    code: AuthenticationCode.EmailOrPasswordIncorrect,
                    message: ErrorMessages.EmailOrPasswordIncorrect
                );
            }

            if (await UserIsLockedOutAsync(user))
            {
                return new AuthenticationResult
                (
                    code: AuthenticationCode.Success,
                    message: ErrorMessages.AccountLocked
                );
            }

            if (await UserPasswordIncorrectAsync(user, loginDto.Password))
            {
                return new AuthenticationResult
                (
                    code: AuthenticationCode.EmailOrPasswordIncorrect,
                    message: ErrorMessages.EmailOrPasswordIncorrect
                );
            }

            await _existingUser.AddRefreshTokenToUserAsync(user.Id);

            return new AuthenticationResult
            (
                code: AuthenticationCode.Success,
                message: _tokenGenerator.CreateToken
                (
                    await _existingUser.GetAccessClaimsForUserAsync(user.Id)
                )
            );
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
