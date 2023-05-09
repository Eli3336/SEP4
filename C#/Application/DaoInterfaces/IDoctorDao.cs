using Domain.Models;

namespace Application.DaoInterfaces;

public interface IDoctorDao
{
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor?> GetByIdNoTrackingAsync(int id);
    Task DeleteAsync(int id);
    Task DoctorUpdateAsync(Doctor doctor);

}