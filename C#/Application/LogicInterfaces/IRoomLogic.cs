using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRoomLogic
{
    Task<Room> CreateAsync(RoomCreationDto roomToCreate);
}