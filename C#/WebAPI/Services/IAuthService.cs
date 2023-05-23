using Domain.Models;


namespace WebAPI.Services;
public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
}