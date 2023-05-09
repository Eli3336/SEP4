﻿using Application.DaoInterfaces;
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
            throw new Exception($"Doctor with ID {id} was not found!");
        }
        
        await receptionistDao.DeleteAsync(id);
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
    
}