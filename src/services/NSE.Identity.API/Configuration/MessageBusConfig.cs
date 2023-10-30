using NSE.MessageBus;
using NSE.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NSE.Identity.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}
