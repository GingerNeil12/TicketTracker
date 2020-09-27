using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TicketTracker.Infrastructure.Security.Models;

namespace TicketTracker.Testing.Security.Common
{
    public abstract class AuthenticationBaseTest
    {
        // TODO: Clean this up.
        protected UserManager<ApplicationUser> CreateMockUserManager()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>(MockBehavior.Strict).Object;
            var options = new Mock<IOptions<IdentityOptions>>(MockBehavior.Strict).Object;
            var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>(MockBehavior.Strict).Object;
            var userValidators = new Mock<IEnumerable<IUserValidator<ApplicationUser>>>(MockBehavior.Strict).Object;
            var passwordValidators = new Mock<IEnumerable<IPasswordValidator<ApplicationUser>>>(MockBehavior.Strict).Object;
            var errors = new Mock<IdentityErrorDescriber>(MockBehavior.Strict).Object;
            var keyNormalizer = new Mock<ILookupNormalizer>(MockBehavior.Strict).Object;
            var services = new Mock<IServiceProvider>(MockBehavior.Strict).Object;
            var logger = new Mock<ILogger<UserManager<ApplicationUser>>>(MockBehavior.Strict).Object;
            return new UserManager<ApplicationUser>
            (
                userStore,
                options,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger
            );
        }
    }
}
