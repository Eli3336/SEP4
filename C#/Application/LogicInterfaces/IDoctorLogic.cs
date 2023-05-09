using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IDoctorLogic
{
    Task<Doctor> CreateAsync(DoctorCreationDto doctorToCreate);
    Task<Doctor?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}