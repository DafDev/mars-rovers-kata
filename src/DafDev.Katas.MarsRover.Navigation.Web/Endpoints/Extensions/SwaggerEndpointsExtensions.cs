using Microsoft.OpenApi.Models;

namespace DafDev.Katas.MarsRover.Web.Endpoints.Extensions;

internal static class SwaggerEndpointsExtensions
{

    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(generator =>
        {
            generator.SwaggerDoc("v1", new OpenApiInfo { Title = "Mars Rover Operation", Version = "v1" });
        });
    }
}