using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
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
        protected static string AdminUserEmail = "admin@email.com";
        protected static string AdminPassword = "Password123!";
        protected static string AdminRoleName = "Administrator";
        protected static string StandardUserEmail = "user@email.com";
        protected static string StandardPassword = "Password!123";
        protected static string StandardRoleName = "User";

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

        protected static UserManager<ApplicationUser> GetNonMockUserManager()
        {
            var provider = GetServiceProvider();
            return (UserManager<ApplicationUser>)provider.GetRequiredService(typeof(UserManager<ApplicationUser>));
        }

        protected static RoleManager<IdentityRole> GetNonMockRoleManager()
        {
            var provider = GetServiceProvider();
            return (RoleManager<IdentityRole>)provider.GetRequiredService(typeof(RoleManager<IdentityRole>));
        }

        protected static JwtSecurityToken GetTokenObjectFromTokenString(string tokenString)
        {
            return new JwtSecurityToken(tokenString);
        }

        protected async Task CreateAdminAccount()
        {
            var user = new ApplicationUser()
            {
                Email = AdminUserEmail,
                UserName = AdminUserEmail
            };

            var userManager = GetNonMockUserManager();
            await userManager.CreateAsync(user, AdminPassword);

            var roleManager = GetNonMockRoleManager();
            
            var adminRole = new IdentityRole(AdminRoleName);
            if(!await roleManager.RoleExistsAsync(AdminRoleName))
            {
                await roleManager.CreateAsync(adminRole);
            }

            if(!await userManager.IsInRoleAsync(user, AdminRoleName))
            {
                await userManager.AddToRoleAsync(user, AdminRoleName);
            }
        }

        protected async Task CreateStandardUserAccount()
        {
            var user = new ApplicationUser()
            {
                Email = StandardUserEmail,
                UserName = StandardUserEmail
            };

            var userManager = GetNonMockUserManager();
            await userManager.CreateAsync(user, StandardPassword);

            var roleManager = GetNonMockRoleManager();

            var standardRole = new IdentityRole(StandardRoleName);
            if (!await roleManager.RoleExistsAsync(StandardRoleName))
            {
                await roleManager.CreateAsync(standardRole);
            }

            if (!await userManager.IsInRoleAsync(user, StandardRoleName))
            {
                await userManager.AddToRoleAsync(user, StandardRoleName);
            }
        }
    }
}
