using Domain.Models;

namespace Application.DaoInterfaces;

public interface IRequestDao
{
    Task<Request> CreateAsync(Request request);
    Task<Request?> GetByIdAsync(int id);
    Task DeleteAsync(Request request);
    Task<IEnumerable<Request>> GetAllRequests();
}