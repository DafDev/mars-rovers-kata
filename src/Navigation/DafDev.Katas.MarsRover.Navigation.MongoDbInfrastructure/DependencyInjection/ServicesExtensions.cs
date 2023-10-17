using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
    {
        services.Configure<MongoSettings>(configurationRoot.GetSection(nameof(MongoSettings)));
    }

    public static void AddConfiguration(this IConfigurationBuilder configuration, IHostEnvironment environment)
    {
        configuration.Sources.Clear();
        configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true);
    }
}
