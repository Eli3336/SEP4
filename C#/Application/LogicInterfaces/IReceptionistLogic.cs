using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IReceptionistLogic
{
    Task<Receptionist> CreateAsync(ReceptionistCreationDto receptionistToCreate);
    Task<Receptionist?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task ReceptionistUpdateAsync(int id, string name, string phoneNumber);
}