using DafDev.Katas.MarsRover.Navigation.Application.Services;
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
        app.MapDelete("/rovers", DecommissionAllRovers);
    }

    public async Task<IResult> LandRoverOnMars(IRoverServices roverServices)
    {
        var rover = await roverServices.LandRover();
        return Results.Ok(rover);
    }

    public async Task<IResult> GetAllRovers(IRoverServices roverServices)
    {
        var rovers = await roverServices.GetAllRovers();
        return Results.Ok(rovers);
    }

    public async Task<IResult> GetRoverById(IRoverServices roverServices, Guid id)
    {
        var rover = await roverServices.GetRoverById(id);
        return Results.Ok(rover);
    }

    public async Task<IResult> DriveRover(IRoverServices roverServices, Guid id, [FromQuery] string commands)
    {
        var rover = await roverServices.DriveRover((await roverServices.GetRoverById(id)).ToDomain(), commands);
        return Results.Ok(rover);
    }

    public async Task<IResult> DecommissionRover(IRoverServices roverServices, Guid id)
    {
        await roverServices.DecommissionRover(id);
        return Results.Ok($"rover with id: {id} sucessfully decommissioned");
    }

    public async Task<IResult> DecommissionAllRovers(IRoverServices roverServices)
    {
        await roverServices.DecommissionAllRovers();
        return Results.Ok($"every rover were sucessfully decommissioned");
    }

}
