using Domain.DTOs;
using Domain.Models;
using Shared;
using Shared.DTOs;

namespace ShopApplication.LogicInterfaces;

public interface IUserLogic
{
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDto? searchParameters);
    Task<User> GetByUsernameAsync(string userName);
    Task<string> Seed();
    
}