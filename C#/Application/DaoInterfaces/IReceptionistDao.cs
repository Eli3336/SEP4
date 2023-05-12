using Domain.Models;

namespace Application.DaoInterfaces;

public interface IReceptionistDao
{
    Task<Receptionist> CreateAsync(Receptionist receptionist);
    Task<Receptionist?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task ReceptionistUpdateAsync(Receptionist receptionist);
    Task<Receptionist?> GetByIdToUpdateAsync(int? id);
    Task<IEnumerable<Receptionist?>> GetAllReceptionistsAsync();
}