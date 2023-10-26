using DafDev.Katas.MarsRover.Navigation.Application.DependencyInjection;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.DependencyInjection;
using DafDev.Katas.MarsRover.Web.Endpoints;
using DafDev.Katas.MarsRover.Web.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfiguration();

// add services 
builder.Services
    .AddConfiguration(builder.Configuration)
    .AddInfraDependencies()
    .AddDependencies()
    .AddSwaggerServices()
    .AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();

app.UseEndpointDefinitions();

app.Run();
