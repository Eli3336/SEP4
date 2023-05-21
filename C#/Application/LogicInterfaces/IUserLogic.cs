using Shared;
using Shared.DTOs;

namespace ShopApplication.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
    Task<User> GetByUsernameAsync(string userName);
    Task<string> Seed();
    
}