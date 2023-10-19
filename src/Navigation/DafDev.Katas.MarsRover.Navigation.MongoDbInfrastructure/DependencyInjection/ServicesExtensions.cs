using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Repository;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfigurationRoot configurationRoot)
    {
        services.Configure<MongoSettings>(options => configurationRoot.GetSection(nameof(MongoSettings)).Bind(options));
        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IRoverRepository, MongoRepository>();
        return services;
    }

    public static void AddConfiguration<T>(this T configuration) where T: IConfigurationRoot,IConfigurationBuilder
    {
        configuration.Sources.Clear();
        configuration.AddUserSecrets<Program>();
        configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    }
}
