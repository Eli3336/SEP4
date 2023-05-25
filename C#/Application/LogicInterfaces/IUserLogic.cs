using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDto? searchParameters);
    Task<User> GetByUsernameAsync(string userName);
    Task<string> Seed();
    
}