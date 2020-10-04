using Microsoft.Extensions.DependencyInjection;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Infrastructure.Security.Interfaces;
using TicketTracker.Infrastructure.Security.Services;

namespace TicketTracker.Infrastructure.Security
{
    public static class DependencyInjection
    {
        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddTransient<IExistingUser, ExistingUser>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IAuthenticator, Authenticator>();
        }
    }
}
