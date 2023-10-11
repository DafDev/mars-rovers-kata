using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using DafDev.Katas.MarsRover.Navigation.Application.Services;
using DafDev.Katas.MarsRover.Navigation.Application.Mappers;
using DafDev.Katas.MarsRover.Navigation.Domain.Services;

namespace DafDev.Katas.MarsRover.Navigation.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDriverServices, DriverServices>();
        services.AddScoped<IRoverServices, RoverServices>();
        services.AddScoped<IDriverCommandMapper, DriverCommandMapper>();
        services.AddScoped<IRoverToRoverDtoMapper, RoverToRoverDtoMapper>();
    }
}
