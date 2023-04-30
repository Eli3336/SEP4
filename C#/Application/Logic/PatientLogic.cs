using System.Security.AccessControl;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PatientLogic : IPatientLogic
{
    private readonly IRoomDao roomDao;
    private readonly IPatientDao patientDao;

    public PatientLogic(IRoomDao roomDao, IPatientDao patientDao)
    {
        this.roomDao = roomDao;
        this.patientDao = patientDao;
    }

    public async Task<Patient> CreateAndAddToRoomAsync(int roomId, PatientCreationDto dto)
    {
        ValidatePatient(dto);
        Room? room = await roomDao.GetById(roomId);
        if (room == null)
            throw new Exception($"Room with id {roomId} not found");
        if (room.Capacity <= room.Patients.Count)
        {
            throw new Exception("Room is full. Cannot add more patients!");
        }

        Patient toCreate = new Patient()
        {
            Name = dto.Name
        };
        Patient created = await patientDao.CreateAsync(toCreate);
        room.Patients.Add(created);
  
        int capacity = room.Capacity;
        int actual = room.Patients.Count;
        if (capacity == actual)
        {
            room.Availability = "Occupied";
        } 
        await roomDao.RoomUpdateAsync(room);
        return created;
    }

    private void ValidatePatient(PatientCreationDto dto)
    {
        if (dto.Name == "")
            throw new ArgumentException("The name cannot be empty!");
        if (dto.Name.Length < 3)
            throw new ArgumentException("The name cannot be smaller than 3 characters!");
        if (dto.Name.Length > 255)
            throw new ArgumentException("The name is too long!");
    }
    
    public async Task DeleteAsync(int id)
    {
        Patient? patient = await patientDao.GetByIdAsync(id);
        if (patient == null)
        {
            throw new Exception($"Patient with ID {id} was not found!");
        }
        Room? room = await roomDao.GetRoomWithPatientId(id);
        if (room == null)
            throw new Exception("Room not found");
        bool wasFull = room.Availability == "Occupied";
        await patientDao.DeleteAsync(id);
        
        if (wasFull)
        {
            room.Availability = "Available";
        } 
        await roomDao.RoomUpdateAsync(room);
    }
    private bool IsRoomNotFull(int roomId)
    {
        bool ok = false;
        Room? room = roomDao.GetByIdToUpdateAsync(roomId).Result;
        if (room != null)
        {
            int capacity = room.Capacity;
            int actual = room.Patients.Count;
            if (actual == capacity - 1)
                ok = true;
        }
        return ok;
    }
    
    public async Task<Patient?> GetByIdAsync(int id)
    {
        Patient? product = await patientDao.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception(
                $"Patient with id {id} not found!");
        }
        return product;
    }
}