﻿using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRoomDao
{
    Task<Room> CreateAsync(Room room);
    Task<Room?> GetById(int id);
    Task<Room?> GetRoomDetailsByIdAsync(int id);
    IEnumerable<string> GetAllNames();
    Task<Patient> CreateAndAddToRoomAsync(int roomId, Patient patient);
}