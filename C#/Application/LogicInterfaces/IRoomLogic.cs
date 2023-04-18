using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRoomLogic
{
    Task<Room> CreateAsync(RoomCreationDto roomToCreate);
    IEnumerable<string> GetAllNames();

    Task<Room?> GetRoomDetailsByNameAsync(string name);
}