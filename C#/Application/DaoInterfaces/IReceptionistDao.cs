﻿using Domain.Models;

namespace Application.DaoInterfaces;

public interface IReceptionistDao
{
    Task<Receptionist> CreateAsync(Receptionist receptionist);
    Task<Receptionist?> GetByIdAsync(int id);
    Task DeleteAsync(Receptionist receptionist);
    Task ReceptionistUpdateAsync(Receptionist receptionist);
    Task<Receptionist?> GetByIdToUpdateAsync(int? id);
    Task<IEnumerable<Receptionist?>> GetAllReceptionistsAsync();
}