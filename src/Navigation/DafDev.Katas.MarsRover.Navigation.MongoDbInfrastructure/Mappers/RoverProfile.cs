using AutoMapper;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Entities;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Mappers;
public class RoverProfile : Profile
{
    public RoverProfile()
    {
        CreateMap<Rover, RoverEntity>()
            .ForMember("Id", desitnation => desitnation.MapFrom(src => src.Id))
            .ReverseMap();
        //CreateMap<CardinalDirections, CardinalDirectionsEntity>();
        CreateMap<CardinalDirections, CardinalDirectionsEntity>().ReverseMap();
        //CreateMap<Coordinates, CoordinatesEntity>();
        CreateMap<Coordinates, CoordinatesEntity>().ReverseMap();
    }
}
