using DafDev.Katas.MarsRover.Application.DependencyInjection;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.DependencyInjection;
using DafDev.Katas.MarsRover.Web.Endpoints;
using DafDev.Katas.MarsRover.Web.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

// add services 
builder.Services.AddDependencies();
builder.Services.AddRepositories();
builder.Services.AddSwaggerServices();
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));

var app = builder.Build();

app.UseEndpointDefinitions();

app.Run();
