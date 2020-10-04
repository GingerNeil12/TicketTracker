using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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
    }
}
