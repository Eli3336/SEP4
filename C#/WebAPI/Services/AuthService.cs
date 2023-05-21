using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;
using EfcDataAccess;
using Shared;
using Shared.DTOs;
using WebAPI.Services;

namespace WebAPI.Services;


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

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    
}