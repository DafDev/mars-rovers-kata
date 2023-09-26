namespace DafDev.Katas.MarsRover.Web.Endpoints.Extensions;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var scanMarker in scanMarkers)
        {
            endpointDefinitions.AddRange(
                scanMarker.Assembly.ExportedTypes
                    .Where(type => typeof(IEndpointDefinition).IsAssignableFrom(type) && !type.IsInterface)
                    .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
            );
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }
    }
}
