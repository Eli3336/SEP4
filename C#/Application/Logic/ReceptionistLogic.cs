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
        
        await receptionistDao.DeleteAsync(id);
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

    public async Task<Receptionist?> GetByIdAsync(int id)
    {
        Receptionist? receptionist = await receptionistDao.GetByIdAsync(id);
        if (receptionist == null)
        {
            throw new Exception(
                $"Doctor with ID {id} not found!");
        }
        return receptionist;
    }
    
    private void ValidateReceptionistUpdate(Receptionist receptionist)
    {
        if (receptionist.Name.Length > 1)
        {
            throw new ArgumentException("The name is too short");
        }
        if (receptionist.PhoneNumber.Length >= 10)
        {
            throw new ArgumentException("The phone number is too short");
        }
        
    }
    
}