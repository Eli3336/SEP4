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
        await doctorDao.DeleteAsync(doctor);
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

    public async Task DoctorUpdateAsync(int id, string name, string phoneNumber)
    {
        Doctor? existing = await doctorDao.GetByIdNoTrackingAsync(id);
        if (existing == null)
        {
            throw new Exception($"Doctor with ID {id} not found!");
        }
        
        DoctorUpdateDto dto = new DoctorUpdateDto(id, name, phoneNumber);

        string nameToUse = dto.Name ?? existing.Name;
        string phoneNumberToUse = dto.PhoneNumber ?? existing.PhoneNumber;
        
        Doctor updated = new (nameToUse, phoneNumberToUse)
        {
            Id = existing.Id,
            Password = existing.Password
        };
        ValidateDoctor(updated);
        await doctorDao.DoctorUpdateAsync(updated);
    }

    private void ValidateDoctor(Doctor doctor)
    {
        if (doctor.Name.Contains("0") || doctor.Name.Contains("1") || doctor.Name.Contains("2") || doctor.Name.Contains("3") || doctor.Name.Contains("4") || doctor.Name.Contains("5") || doctor.Name.Contains("6") || doctor.Name.Contains("7") || doctor.Name.Contains("8") || doctor.Name.Contains("9"))
            throw new Exception("Name cannot contain numbers!");
        if (doctor.Name.Length < 3)
            throw new Exception("Name too short!");
        if (doctor.Name.Length > 255)
            throw new Exception("Name too long!");
        if (doctor.Password.Length < 3)
            throw new Exception("Password too short!");
        if (doctor.Password.Length > 255)
            throw new Exception("Password too long!");
        if (doctor.PhoneNumber.Length < 6)
            throw new Exception("Phone number too short!");
        if (doctor.PhoneNumber.Length > 13)
            throw new Exception("Phone number too long!");
        IEnumerable<Doctor?> doctors = doctorDao.GetAllDoctorsAsync().Result;
        if (doctors.Contains(doctor))
        {
            throw new Exception("Doctor "+ doctor.Name + " already exists!");
        }
        
    }
    
    public Task<IEnumerable<Doctor?>> GetAllDoctorsAsync()
    {
        IEnumerable<Doctor?> doctors = doctorDao.GetAllDoctorsAsync().Result; 
        return Task.FromResult(doctors);
    }
}
   