using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRoomLogic
{
    Task<Room> CreateAsync(RoomCreationDto roomToCreate);
    IEnumerable<string> GetAllNames();
    Task<Room?> GetRoomDetailsByIdAsync(int id);
    Task RoomUpdateAsync(int id, string name, int capacity, string availability);
    Task<IEnumerable<Room?>> GetAllRoomsAsync();
    Task<IEnumerable<Room>> GetAllEmptyRooms();
    Task<IEnumerable<Room>> GetAllAvailableRooms();
}