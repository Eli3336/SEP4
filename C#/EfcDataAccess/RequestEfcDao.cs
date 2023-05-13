using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class RequestEfcDao: IRequestDao
{
    private readonly HospitalContext context;

    public RequestEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    
    public async Task<Request?> GetByIdAsync(int id)
    {
        Request? found = await context.Request
            .SingleOrDefaultAsync(doctor => doctor.Id == id);
        return found;
    }
    
    public async Task DeleteAsync(int id)
    {
        Request? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Request with id {id} not found");
        }
        context.Request.Remove(existing);
        await context.SaveChangesAsync();  
    }
    
    public async Task<IEnumerable<Request>> GetAllRequests()
    {
        IQueryable<Request> receptionists = context.Request.AsQueryable();
        IEnumerable<Request> result = await receptionists.ToListAsync();
        return result;
    }
}