using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class RoomLogic : IRoomLogic
{
    private readonly IRoomDao roomDao;

    public RoomLogic(IRoomDao roomDao)
    {
        this.roomDao = roomDao;
    }
    
    public async Task<Room> CreateAsync(RoomCreationDto roomToCreate)
    {
        Room toCreate = new Room()
        {
            Name = roomToCreate.Name,
            Sensors = roomToCreate.Sensors
        };
    
        Room created = await roomDao.CreateAsync(toCreate);
    
        return created;
    }
}