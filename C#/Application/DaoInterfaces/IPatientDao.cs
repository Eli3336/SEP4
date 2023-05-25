using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPatientDao
{
    Task<Patient?> GetByIdAsync(int id);
    Task DeleteAsync(Patient patient);
    Task<Patient> CreateAsync(Patient patient);
    Task<IEnumerable<Patient?>> GetAllPatientsAsync();

}