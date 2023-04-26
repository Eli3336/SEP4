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
        Room? room = await context.Rooms.FindAsync(id);
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

    public async Task<Patient> CreateAndAddToRoomAsync(int roomId, Patient patient)
    {
        Room? room = await context.Rooms.FindAsync(roomId);
        if (room == null)
            throw new Exception($"Room with id {roomId} not found");
        EntityEntry<Patient> newPatient = await context.Patients.AddAsync(patient);
        context.Rooms.Find(roomId).Patients.Add(patient);
        await context.SaveChangesAsync();
        return newPatient.Entity;
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
}
    
   
    


