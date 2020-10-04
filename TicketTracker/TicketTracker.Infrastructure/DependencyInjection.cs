using Microsoft.Extensions.DependencyInjection;
using TicketTracker.Application.Interfaces.Common;
using TicketTracker.Infrastructure.Services;

namespace TicketTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICurrentDateTime, CurrentDateTime>();
        }
    }
}
