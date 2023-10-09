using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Repository;
public interface IRoverRepository
{
    Rover Create(Rover? rover = null);
    Rover Get(Guid id);
    Rover Update(Rover rover);
    void Delete(Guid id);
    IEnumerable<Rover> GetAll();
}
