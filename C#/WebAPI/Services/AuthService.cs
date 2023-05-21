using System.ComponentModel.DataAnnotations;
using Domain.Models;
using EfcDataAccess;
using Shared;
using Shared.DTOs;

namespace Shop.Services;


public class AuthService:IAuthService
{

    private readonly IUserDao userDao;

    public AuthService(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    private readonly IList<User> users = new List<User>
    {
        
    };
    public async Task<User> ValidateUser(string username, string password)
    {

        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    public async Task<User> RegisterUser(UserCreationDto dto)
    {

        if (await userDao.GetByUsernameAsync(dto.UserName) != null)
        {
            throw new ValidationException("Username already exists");
        }
        
        if (string.IsNullOrEmpty(dto.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        User user;
        if(dto.UserName.Equals("admin"))
            user = new User(dto.Name, dto.PhoneNumber, dto.UserName, dto.Password,"administrator");
        else
            user = new User(dto.Name, dto.PhoneNumber, dto.UserName, dto.Password, "");
        
        users.Add(user);
        User userToCreate = await userDao.CreateAsync(user);
        return userToCreate;
    }
}