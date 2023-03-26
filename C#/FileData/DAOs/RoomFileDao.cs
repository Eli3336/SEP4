using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class RoomFileDao : IRoomDao
{
    private readonly FileContext context;

    public RoomFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Room> CreateAsync(Room room)
    {
        int roomId = 1;
        if (context.Rooms.Any())
        {
            roomId = context.Rooms.Max(u => u.Id);
            roomId++;
        }

        room.Id = roomId;

        context.Rooms.Add(room);
        context.SaveChanges();

        return Task.FromResult(room);
    }
    
    public Task<Room?> GetById(int id)
    {
        Room? existing = context.Rooms.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(existing);
    }
}