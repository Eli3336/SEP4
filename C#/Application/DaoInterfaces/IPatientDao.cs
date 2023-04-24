using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPatientDao
{
    Task<Patient?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}