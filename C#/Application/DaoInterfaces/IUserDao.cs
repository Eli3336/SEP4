using Domain.DTOs;
using Domain.Models;
using Shared;
using Shared.DTOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDto? searchParameters);
    Task<string> Seed();
}