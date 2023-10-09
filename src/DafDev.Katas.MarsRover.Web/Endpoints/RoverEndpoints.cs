using DafDev.Katas.MarsRover.Application.Navigation.Services;
using Microsoft.AspNetCore.Mvc;

namespace DafDev.Katas.MarsRover.Web.Endpoints;

public class RoverEndpoints : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/rovers", LandRoverOnMars);
        app.MapGet("/rovers", GetAllRovers);
        app.MapGet("/rovers/{id}", GetRoverById);
        app.MapPut("/rovers/drive/{id}", DriveRover);
        app.MapDelete("/rovers/{id}", DecommissionRover);
    }

    public IResult LandRoverOnMars(IRoverServices roverServices)
    {
        var rover = roverServices.LandRover();
        return Results.Ok(rover);
    }

    public IResult GetAllRovers(IRoverServices roverServices)
    {
        var rovers = roverServices.GetAllRovers();
        return Results.Ok(rovers);
    }

    public IResult GetRoverById(IRoverServices roverServices, Guid id)
    {
        var rover = roverServices.GetRoverById(id);
        return Results.Ok(rover);
    }

    public IResult DriveRover(IRoverServices roverServices, Guid id, [FromQuery] string commands)
    {
        var rover = roverServices.DriveRover(roverServices.GetRoverById(id), commands);
        return Results.Ok(rover);
    }

    public IResult DecommissionRover(IRoverServices roverServices, Guid id)
    {
        roverServices.DecommissionRover(id);
        return Results.Ok($"rover with id: {id} sucessfully decommissioned");
    }
}
