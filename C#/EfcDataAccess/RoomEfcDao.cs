using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class RoomEfcDao : IRoomDao
{
    private readonly HospitalContext context;

    public RoomEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    public async Task<Room> CreateAsync(Room room)
    {
        EntityEntry<Room> newRoom = await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
        return newRoom.Entity;
    }

    public async Task<Room?> GetById(int id)
    {
        Room? room = await context.Rooms.Include(room => room.Patients).Include(room => room.Sensors).SingleOrDefaultAsync(room => room.Id == id);
        return room;
    }
    public IEnumerable<string> GetAllNames()
    {
        List<string> names = new List<string>();
        List<Room> rooms = context.Rooms.ToList();

        for (int i = 0; i < rooms.Count; i++)
        {
            names.Add(rooms[i].Name);
        }
        return names;
    }
    public async Task<Room?> GetRoomDetailsByIdAsync(int id)
    {
        Room? room = await context.Rooms.Include(room => room.Sensors).Include(room => room.Patients).SingleOrDefaultAsync(room => room.Id==id);
        return room;
    }
    
    public async Task<Room?> GetRoomWithPatientId(int patientId)
    {
        Room? room = await context.Rooms.Include(room => room.Patients).SingleOrDefaultAsync(room => room.Patients.Any(patient => patient.Id == patientId));
        return room;
    }

    public async Task RoomUpdateAsync(Room room)
    {
        context.Rooms.Update(room);
        await context.SaveChangesAsync();

    }
    public async  Task<Room?> GetByIdToUpdateAsync(int? id)
    {
        Room? found = await context.Rooms
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        return found;
    }
    public List<Room> GetAllRoomsWithPatientsNotSensors()
    {
        return context.Rooms.AsNoTracking().Include(room => room.Patients).ToList();
    }
}
    
   
    


