﻿using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class RequestEfcDao: IRequestDao
{
    private readonly HospitalContext context;

    public RequestEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    
    public async Task<Request> CreateAsync(Request request)
    {
        EntityEntry<Request> newRequest = await context.Request.AddAsync(request);
        await context.SaveChangesAsync();
        return newRequest.Entity;
    }
    
    public async Task<Request?> GetByIdAsync(int id)
    {
        Request? found = await context.Request
            .SingleOrDefaultAsync(doctor => doctor.Id == id);
        return found;
    }
    
    public async Task DeleteAsync(Request request)
    {
        context.Request.Remove(request);
        await context.SaveChangesAsync();  
    }
    
    public async Task<IEnumerable<Request>> GetAllRequests()
    {
        IQueryable<Request> receptionists = context.Request.AsQueryable();
        IEnumerable<Request> result = await receptionists.ToListAsync();
        return result;
    }
}