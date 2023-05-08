using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class DoctorLogic : IDoctorLogic
{
    private readonly IDoctorDao doctorDao;

    public DoctorLogic(IDoctorDao doctorDao)
    {
        this.doctorDao = doctorDao;
    }
    
    public async Task<Doctor> CreateAsync(DoctorCreationDto doctorToCreate)
    {
        Doctor toCreate = new Doctor()
        {
            Name = doctorToCreate.Name,
            Password = doctorToCreate.Password,
            PhoneNumber = doctorToCreate.PhoneNumber,
        };
    
        Doctor created = await doctorDao.CreateAsync(toCreate);
    
        return created;
    }

    public async Task DeleteAsync(int id)
    {
        Doctor? doctor = await doctorDao.GetByIdAsync(id);
        if (doctor == null)
        {
            throw new Exception($"Doctor with ID {id} was not found!");
        }
        
        await doctorDao.DeleteAsync(id);
    }
    
    public async Task<Doctor?> GetByIdAsync(int id)
    {
        Doctor? doctor = await doctorDao.GetByIdAsync(id);
        if (doctor == null)
        {
            throw new Exception(
                $"Doctor with ID {id} not found!");
        }
        return doctor;
    }
}
   