using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRoverRepository, InMemoryRoverRepository>();
    }
}
