using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRoomDao
{
    Task<Room> CreateAsync(Room room);
    Task<Room?> GetById(int id);
    Task<Room?> GetRoomDetailsByIdAsync(int id);
    IEnumerable<string> GetAllNames();
    Task RoomUpdateAsync(Room room);
    Task<Room?> GetByIdToUpdateAsync(int? id);
    Task<Room?> GetRoomWithPatientId(int patientId);
    List<Room> GetAllRoomsWithPatientsNotSensors();

}