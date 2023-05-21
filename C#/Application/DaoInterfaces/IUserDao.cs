using Domain.Models;
using Shared;
using Shared.DTOs;

namespace ShopApplication.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
    Task<string> Seed();
}