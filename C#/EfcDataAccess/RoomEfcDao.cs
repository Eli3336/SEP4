using Application.DaoInterfaces;
using Domain.Models;
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
    }