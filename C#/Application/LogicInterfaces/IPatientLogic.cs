using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPatientLogic
{
    Task<Patient> CreateAndAddToRoomAsync(int roomId, PatientCreationDto dto);
    Task<Patient?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task MovePatientToGivenRoom(int patientId, int roomId);
    Task<IEnumerable<Patient?>> GetAllPatientsAsync();

}