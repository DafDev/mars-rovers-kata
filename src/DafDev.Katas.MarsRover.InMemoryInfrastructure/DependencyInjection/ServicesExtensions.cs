using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DafDev.Katas.MarsRover.InMemoryInfrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRoverRepository, InMemoryRoverRepository>();
    }
}
