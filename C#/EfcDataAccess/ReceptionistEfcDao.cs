using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class ReceptionistEfcDao : IReceptionistDao
{
    private readonly HospitalContext context;

    public ReceptionistEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    public async Task<Receptionist> CreateAsync(Receptionist receptionist)
    {
        EntityEntry<Receptionist> newReceptionist = await context.Receptionists.AddAsync(receptionist);
        await context.SaveChangesAsync();
        return newReceptionist.Entity;
    }
    

    public async Task<Receptionist?> GetByIdAsync(int id)
    {
        Receptionist? found = await context.Receptionists
            .SingleOrDefaultAsync(r => r.Id == id);
        return found;
    }

    public async Task DeleteAsync(Receptionist receptionist)
    {
        context.Receptionists.Remove(receptionist);
        await context.SaveChangesAsync();  
    }
    
    public async Task ReceptionistUpdateAsync(Receptionist receptionist)
    {
        context.Receptionists.Update(receptionist);
        await context.SaveChangesAsync();

    }

    public async Task<Receptionist?> GetByIdToUpdateAsync(int? id)
    {
        Receptionist? found = await context.Receptionists
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id);

        return found;
    }

    public async Task<IEnumerable<Receptionist?>> GetAllReceptionistsAsync()
    {
         IQueryable<Receptionist> receptionists = context.Receptionists.AsQueryable();
         IEnumerable<Receptionist> result = await receptionists.ToListAsync();
         return result;
    }
}