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
        ValidateDoctor(toCreate);
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
    
    public async Task DoctorUpdateAsync(int id, string name, string password, string phoneNumber)
    {
        Doctor? existing = await doctorDao.GetByIdNoTrackingAsync(id);
        if (existing == null)
        {
            throw new Exception($"Doctor with ID {id} not found!");
        }
        
        DoctorUpdateDto dto = new DoctorUpdateDto(id, name, password, phoneNumber);
        
        string nameToUse = dto.Name ?? existing.Name;
        string passwordToUse = dto.Password ?? existing.Password;
        string phoneNumberToUse = dto.PhoneNumber ?? existing.PhoneNumber;
        
        
        Doctor updated = new (nameToUse, passwordToUse, phoneNumberToUse)
        {
            Id = existing.Id
        };
        ValidateDoctor(updated);
        await doctorDao.DoctorUpdateAsync(updated);
    }

    private void ValidateDoctor(Doctor doctor)
    {
        if (doctor.Name.Length < 3)
            throw new Exception("Name too small!");
        if (doctor.Name.Length > 255)
            throw new Exception("Name too big!");
        if (doctor.Password.Length < 3)
            throw new Exception("Password too small!");
        if (doctor.Password.Length > 255)
            throw new Exception("Password too big!");
        if (doctor.PhoneNumber.Length < 6)
            throw new Exception("Phone number too small!");
        if (doctor.PhoneNumber.Length > 13)
            throw new Exception("Phone number too big!");
    }
}
   