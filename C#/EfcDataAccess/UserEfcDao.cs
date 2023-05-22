using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class UserEfcDao : IUserDao
{
    
    private readonly HospitalContext context;

    public UserEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = null;
        
        IQueryable<Doctor> doctors = context.Doctors.AsQueryable();
        IEnumerable<Doctor> result = await doctors.ToListAsync();
        
        IQueryable<Receptionist> receptionists = context.Receptionists.AsQueryable();
        IEnumerable<Receptionist> receptionistsList = await receptionists.ToListAsync();
        
        IQueryable<Admin> admins = context.Admins.AsQueryable();
        IEnumerable<Admin> adminsList = await admins.ToListAsync();
        
        foreach (var doctor in result)
        {

            if (doctor.Name == userName)
            {
                existing = doctor;
            }
        }
        
        foreach (var receptionist in receptionistsList)
        {

            if (receptionist.Name == userName)
            {
                existing = receptionist;
            }
        }
        foreach (var administrator in adminsList)
        {

            if (administrator.Name == userName)
            {
                existing = administrator;
            }
        }
        
        
        return existing;    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParameterDto? searchParameters)
    {
        IQueryable<User> usersQuery;
        
        usersQuery = context.Admins.AsQueryable();
        if (searchParameters?.UsernameContains != null)
        {
            usersQuery = context.Admins.Where(u =>
                u.Name.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }
        usersQuery = context.Doctors.AsQueryable();
        if (searchParameters?.UsernameContains != null)
        {
            usersQuery = context.Doctors.Where(u =>
                u.Name.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }
        usersQuery = context.Receptionists.AsQueryable();
        if (searchParameters?.UsernameContains != null)
        {
            usersQuery = context.Receptionists.Where(u =>
                u.Name.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }
        IEnumerable<User> result = await usersQuery.ToListAsync();
        return await Task.FromResult(result);    }

    public Task<string> Seed()
    {
        //TODO: Maybe a good method for a demo?
        throw new NotImplementedException();
    }
}