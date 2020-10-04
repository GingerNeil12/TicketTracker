using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TicketTracker.Infrastructure.Security.Models;

namespace TicketTracker.Testing.Security.Common
{
    public abstract class AbstractAuthenticationBase : AbstractConfigurationBase
    {
        // TODO: Clean this up.
        protected static Mock<UserManager<ApplicationUser>> CreateMockUserManager()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>().Object;
            var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>().Object;
            var userValidators = new IUserValidator<ApplicationUser>[0];
            var passwordValidators = new IPasswordValidator<ApplicationUser>[0];
            var errors = new Mock<IdentityErrorDescriber>().Object;
            var keyNormalizer = new Mock<ILookupNormalizer>().Object;
            var services = new Mock<IServiceProvider>().Object;
            var logger = new Mock<ILogger<UserManager<ApplicationUser>>>().Object;

            return new Mock<UserManager<ApplicationUser>>
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

        protected static UserManager<ApplicationUser> GetUserManager()
        {
            var provider = GetServiceProvider();
            return (UserManager<ApplicationUser>)provider.GetRequiredService(typeof(UserManager<ApplicationUser>));
        }
    }
}
