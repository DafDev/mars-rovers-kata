namespace DafDev.Katas.MarsRover.Web.Endpoints;

public class SwaggerEndpoints : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Mars Rover Operation"));
    }
}
