using Shared;
using Shared.DTOs;

namespace Shop.Services;
public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task<User> RegisterUser(UserCreationDto dto);
}