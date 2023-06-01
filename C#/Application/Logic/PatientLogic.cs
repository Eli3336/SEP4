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
        if (!room.Availability.Equals("Available"))
        {
            throw new Exception("Room is not available, cannot add patient!");
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
        if (dto.Name.Contains("0") || dto.Name.Contains("1") || dto.Name.Contains("2") || dto.Name.Contains("3") || dto.Name.Contains("4") || dto.Name.Contains("5") || dto.Name.Contains("6") || dto.Name.Contains("7") || dto.Name.Contains("8") || dto.Name.Contains("9"))
            throw new Exception("Name cannot contain numbers!");
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
        await patientDao.DeleteAsync(patient);
        
        if (wasFull)
        {
            room.Availability = "Available";
        } 
        await roomDao.RoomUpdateAsync(room);
    }
    
    public async Task<Patient?> GetByIdAsync(int id)
    {
        Patient? patient = await patientDao.GetByIdAsync(id);
        if (patient == null)
        {
            throw new Exception(
                $"Patient with id {id} not found!");
        }
        return patient;
    }
    
    public async Task MovePatientToGivenRoom(int patientId, int roomId)
    {
        //getting the new room
        Room? roomToMoveInto = await roomDao.GetById(roomId);
        if (roomToMoveInto == null)
            throw new Exception($"Room with id {roomId} not found");
        if (roomToMoveInto.Capacity <= roomToMoveInto.Patients.Count)
        {
            throw new Exception("Room is full. Cannot add more patients!");
        }

        //getting the patient
        Patient? toMove = await patientDao.GetByIdAsync(patientId);
        if (toMove != null)
        {
            ValidatePatient(new PatientCreationDto(toMove.Name));
        }
        else
            throw new Exception($"Patient with ID {patientId} was not found!");

        //removing from old room
        List<Room?> allRooms = roomDao.GetAllRoomsAsync().Result.ToList();
        for (int i = 0; i < allRooms.Count; i++)
        {
            if (allRooms[i].Patients.Contains(toMove))
            {
                bool wasFull = allRooms[i].Availability == "Occupied";
                if (wasFull)
                {
                    allRooms[i].Availability = "Available";
                } 
                allRooms[i].Patients.Remove(toMove);
                await roomDao.RoomUpdateAsync(allRooms[i]);
                break;
            }
        }   
        
        //adding to new room
        roomToMoveInto.Patients.Add(toMove);
        if (roomToMoveInto.Capacity == roomToMoveInto.Patients.Count)
        {
            roomToMoveInto.Availability = "Occupied";
        }
        await roomDao.RoomUpdateAsync(roomToMoveInto);
    }
    
    public Task<IEnumerable<Patient?>> GetAllPatientsAsync()
    {
        IEnumerable<Patient?> patients = patientDao.GetAllPatientsAsync().Result; 
        return Task.FromResult(patients);
    }
}