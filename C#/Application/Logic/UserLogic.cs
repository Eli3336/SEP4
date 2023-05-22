using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Shared;
using Shared.DTOs;
using ShopApplication.LogicInterfaces;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<string> Seed()
    {
       return await userDao.Seed();
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDto? searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
    
   
    public async Task<User> GetByUsernameAsync(string userName)
    {
        User? user = await userDao.GetByUsernameAsync(userName);
        if (user == null)
        {
            throw new Exception(
                $"User with id {userName} not found!");
        }

        return user;
    }
}