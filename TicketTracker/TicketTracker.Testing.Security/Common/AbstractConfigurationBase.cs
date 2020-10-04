using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketTracker.Infrastructure.Security.Models;
using TicketTracker.Testing.Security.Data;

namespace TicketTracker.Testing.Security.Common
{
    public abstract class AbstractConfigurationBase
    {
        protected static IConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        protected static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDbContext<SecurityDbContext>
            (
                options => options.UseInMemoryDatabase("SecurityTestDb")
            );

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>();

            return services.BuildServiceProvider();
        }
    }
}
