using Application.DaoInterfaces;
using Domain.Models;

namespace EfcDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


public class DoctorEfcDao : IDoctorDao
{
    private readonly HospitalContext context;

    public DoctorEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    public async Task<Doctor> CreateAsync(Doctor doctor)
    {
        EntityEntry<Doctor> newDoctor = await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();
        return newDoctor.Entity;
    }

    public async Task<Doctor?> GetByIdAsync(int id)
    {
        Doctor? found = await context.Doctors
            .SingleOrDefaultAsync(doctor => doctor.Id == id);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Doctor? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Doctor with id {id} not found");
        }
        context.Doctors.Remove(existing);
        await context.SaveChangesAsync();  
    }
}