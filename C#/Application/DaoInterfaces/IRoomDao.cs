using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRoomDao
{
    Task<Room> CreateAsync(Room room);
    Task<Room?> GetById(int id);
}