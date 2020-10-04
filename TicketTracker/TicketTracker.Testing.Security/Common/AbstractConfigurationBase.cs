using Microsoft.Extensions.Configuration;

namespace TicketTracker.Testing.Security.Common
{
    public abstract class AbstractConfigurationBase
    {
        protected static IConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}
