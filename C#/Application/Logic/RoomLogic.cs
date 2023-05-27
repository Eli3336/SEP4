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
        if (roomToCreate.Capacity > 3)
        {
            throw new ArgumentException("The capacity cannot be bigger than 3");
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

    public async Task<Room?> GetRoomDetailsByIdAsync(int id)
    {
        Room? room = await roomDao.GetRoomDetailsByIdAsync(id);
        if (room == null)
        {
            throw new Exception(
                $"Room with the id {id} not found!");
        }
        return room;
    }
    
    public async Task RoomUpdateAsync(int id, string name, int capacity, string availability)
    {
        Room? existing = await roomDao.GetByIdToUpdateAsync(id);
        if (existing == null)
        {
            throw new Exception($"Room with ID {id} not found!");
        }
        
        RoomUpdateDto dto = new RoomUpdateDto(id, name, capacity, availability);

        string nameToUse = dto.Name ?? existing.Name;
        int capacityToUse = dto.Capacity ?? existing.Capacity;
        string statusToUse = dto.Availability ?? existing.Availability;
        
        
        Room updated = new (nameToUse, capacityToUse, statusToUse)
        {
            Id = existing.Id,
            Patients = existing.Patients,
            Sensors = existing.Sensors
            
        };
        ValidateRoomUpdate(updated);
        await roomDao.RoomUpdateAsync(updated);
    }

    public Task<IEnumerable<Room?>> GetAllRoomsAsync()
    {
        IEnumerable<Room?> rooms = roomDao.GetAllRoomsAsync().Result;
        return Task.FromResult(rooms);
    }

    public async Task<IEnumerable<Room>> GetAllEmptyRooms()
    {
        List<Room> result = new List<Room>();
        List<Room?> rooms = roomDao.GetAllRoomsAsync().Result.ToList();
        if (rooms.Count < 1)
        {
            return result;
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            if (room.Patients.Count == 0)
                    result.Add(room);
        }
        return result;
    }
    
    public async Task<IEnumerable<Room>> GetAllAvailableRooms()
    {
        List<Room> result = new List<Room>();
        List<Room?> rooms = roomDao.GetAllRoomsAsync().Result.ToList();
        if (rooms.Count < 1)
        {
            return result;
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            if (room.Availability.Equals("Available"))
                result.Add(room);
        }
        return result;
    }

    private void ValidateRoomUpdate(Room room)
    {
        if (room.Capacity < 1)
        {
            throw new ArgumentException("The capacity cannot be smaller than 1");
        }
        if (room.Capacity > 3)
        {
            throw new ArgumentException("The capacity cannot be bigger than 3");
        }
        if (room.Availability != "Available" && room.Availability != "Under maintenance")
        {
            throw new ArgumentException("The room can only be Available or Under maintenance. Please choose one or check for typos!");
        }
    }
   
}