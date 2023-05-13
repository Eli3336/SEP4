using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRequestDao
{
    Task<Request?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<Request>> GetAllRequests();
}