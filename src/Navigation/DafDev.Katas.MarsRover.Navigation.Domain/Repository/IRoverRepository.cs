using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Domain.Repository;
public interface IRoverRepository
{
    Task<Rover> Create(Rover? rover = null);
    Task<Rover> Get(Guid id);
    Task<IEnumerable<Rover>> GetAll();
    Task<Rover> UpdateOrCreate(Rover rover);
    Task Delete(Guid id);
    Task DeleteAll();
}
