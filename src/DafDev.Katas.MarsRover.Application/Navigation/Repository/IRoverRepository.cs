using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Repository;
public interface IRoverRepository
{
    Task<Rover> Create(Rover? rover = null);
    Task<Rover> Get(Guid id);
    Task<Rover> Update(Rover rover);
    Task Delete(Guid id);
    Task<IEnumerable<Rover>> GetAll();
}
