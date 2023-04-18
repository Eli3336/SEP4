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
        ValidateRoom(roomToCreate);
        Room toCreate = new Room()
        {
            Name = roomToCreate.Name,
            Capacity = roomToCreate.Capacity,
            Availability = roomToCreate.Availability,
            Sensors = roomToCreate.Sensors
        };
    
        Room created = await roomDao.CreateAsync(toCreate);
    
        return created;
    }

    private void ValidateRoom(RoomCreationDto roomToCreate)
    {
        if (roomToCreate.Capacity < 1)
        {
            throw new ArgumentException("The capacity cannot be smaller than 1");
        }
        if (roomToCreate.Availability != "Available" && roomToCreate.Availability != "Under maintenance")
        {
            throw new ArgumentException("The room can only be Available or Under maintenance. Please choose one or check for typos!");
        }
        List<Sensor> sensorsInRoom = roomToCreate.Sensors;
        if (sensorsInRoom.Count != 3)
            throw new ArgumentException("The room has less or more than 3 sensors. Please choose 3 sensors.");
        if (!sensorsInRoom[0].Type.Equals("Temperature"))
            throw new ArgumentException("The first sensor is not the temperature one. Please make sure that you have the sensors in the correct order: Temperature, Humidity, CO2; and that there are no misspellings!");
        if (!sensorsInRoom[1].Type.Equals("Humidity"))
            throw new ArgumentException("The first sensor is not the humidity one. Please make sure that you have the sensors in the correct order: Temperature, Humidity, CO2; and that there are no misspellings!");
        if (!sensorsInRoom[2].Type.Equals("CO2"))
            throw new ArgumentException("The first sensor is not the CO2 one. Please make sure that you have the sensors in the correct order: Temperature, Humidity, CO2; and that there are no misspellings!");
    }
    public IEnumerable<string> GetAllNames()
    {
        return roomDao.GetAllNames();
    }

    public async Task<Room?> GetRoomDetailsByNameAsync(string name)
    {
        Room? room = await roomDao.GetRoomDetailsByNameAsync(name);
        if (room == null)
        {
            throw new Exception(
                $"Room with name {name} not found!");
        }
        return room;
    }

}