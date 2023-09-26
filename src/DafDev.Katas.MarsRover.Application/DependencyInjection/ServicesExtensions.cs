using DafDev.Katas.MarsRover.Application.Navigation.Services;
using DafDev.Katas.MarsRover.Application.Navigation.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DafDev.Katas.MarsRover.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDriverServices, DriverServices>();
        services.AddScoped<IRoverServices, RoverServices>();
        services.AddScoped<IDriverCommandMapper, DriverCommandMapper>();
    }
}
