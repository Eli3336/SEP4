using Domain.Models;

namespace Application.DaoInterfaces;

public interface IDoctorDao
{
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}