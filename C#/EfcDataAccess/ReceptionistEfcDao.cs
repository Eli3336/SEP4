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
            .SingleOrDefaultAsync(doctor => doctor.Id == id);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Receptionist? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Doctor with id {id} not found");
        }
        context.Receptionists.Remove(existing);
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
}