using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ReceptionistLogic : IReceptionistLogic
{
    private readonly IReceptionistDao receptionistDao;

    public ReceptionistLogic(IReceptionistDao receptionistDao)
    {
        this.receptionistDao = receptionistDao;
    }
    
    public async Task<Receptionist> CreateAsync(ReceptionistCreationDto receptionistToCreate)
    {
        Receptionist toCreate = new Receptionist()
        {
            Name = receptionistToCreate.Name,
            Password = receptionistToCreate.Password,
            PhoneNumber = receptionistToCreate.PhoneNumber,
        };
        Receptionist created = await receptionistDao.CreateAsync(toCreate);
        return created;
    }

    public async Task DeleteAsync(int id)
    {
        Receptionist? receptionist = await receptionistDao.GetByIdAsync(id);
        if (receptionist == null)
        {
            throw new Exception($"Receptionist with ID {id} was not found!");
        }
        await receptionistDao.DeleteAsync(receptionist);
    }

    public async Task ReceptionistUpdateAsync(int id, string name, string phoneNumber)
    {
        Receptionist? existing = await receptionistDao.GetByIdToUpdateAsync(id);
        if (existing == null)
        {
            throw new Exception($"Receptionist with ID {id} not found!");
        }
        ReceptionistUpdateDto dto = new ReceptionistUpdateDto(name, phoneNumber);

        string nameToUse = dto.Name ?? existing.Name;
        string phoneNumberToUse = dto.PhoneNumber ?? existing.PhoneNumber;

        Receptionist updated = new ()
        {
            Id = existing.Id,
            Name = nameToUse,
            Password = existing.Password,
            PhoneNumber = phoneNumberToUse
        };
        ValidateReceptionistUpdate(updated);
        await receptionistDao.ReceptionistUpdateAsync(updated);
    }

    public Task<IEnumerable<Receptionist?>> GetAllReceptionistsAsync()
    {
        IEnumerable<Receptionist?> receptionists = receptionistDao.GetAllReceptionistsAsync().Result; 
        return Task.FromResult(receptionists);
    }

    public async Task<Receptionist?> GetByIdAsync(int id)
    {
        Receptionist? receptionist = await receptionistDao.GetByIdAsync(id);
        if (receptionist == null)
        {
            throw new Exception(
                $"Receptionist with ID {id} not found!");
        }
        return receptionist;
    }
    
    private void ValidateReceptionistUpdate(Receptionist receptionist)
    {
       if (receptionist.Name.Contains("0") || receptionist.Name.Contains("1") || receptionist.Name.Contains("2") || receptionist.Name.Contains("3") || receptionist.Name.Contains("4") || receptionist.Name.Contains("5") || receptionist.Name.Contains("6") || receptionist.Name.Contains("7") || receptionist.Name.Contains("8") || receptionist.Name.Contains("9"))
            throw new Exception("Name cannot contain numbers!");
       if (receptionist.Name.Length < 3)
            throw new Exception("Name too short!");
       if (receptionist.Name.Length > 255)
            throw new Exception("Name too long!");
       if (receptionist.PhoneNumber.Length < 6)
            throw new Exception("Phone number too short!"); 
       if (receptionist.PhoneNumber.Length > 13)
            throw new Exception("Phone number too long!");
    }
}