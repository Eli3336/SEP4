using Domain.Models;
using Shared;
using Shared.DTOs;
using ShopApplication.DaoInterfaces;
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

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
       User? existing = await userDao.GetByUsernameAsync(dto.UserName);
       if (existing != null)
           throw new Exception("Username already taken!");
               
        try
        {
            ValidateData(dto);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        User toCreate = new User
        {
            name = dto.Name,
            phoneNumber = dto.PhoneNumber,
            username = dto.UserName,
            password = dto.Password
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string name = userToCreate.Name;
        string phoneNumber = userToCreate.PhoneNumber;
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;

        if (name.Length < 3)
            throw new Exception("Name must be at least 3 characters!");

        if (name.Length > 50)
            throw new Exception("Name must be less than 50 characters!");
        
        if (phoneNumber.Length < 3)
            throw new Exception("Phone number must be at least 3 characters!");

        if (phoneNumber.Length > 30)
            throw new Exception("Phone number must be less than 16 characters!");
        
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if (password.Length < 3)
            throw new Exception("Password must be at least 3 characters!");

        if (password.Length > 20)
            throw new Exception("Password must be less than 20 characters!");
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