using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRequestDao
{
    Task<Request?> GetByIdAsync(int id);
    Task DeleteAsync(Request request);
    Task<IEnumerable<Request>> GetAllRequests();
}