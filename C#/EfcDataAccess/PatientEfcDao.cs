using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;
public class PatientEfcDao : IPatientDao
{
    private readonly HospitalContext context;

    public PatientEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    public async Task DeleteAsync(int id)
    {
        Patient? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Patient with id {id} not found");
        }
        context.Patients.Remove(existing);
        await context.SaveChangesAsync();    
    }
    
    public async Task<Patient?> GetByIdAsync(int id)
    {
        Patient? found = await context.Patients
            .SingleOrDefaultAsync(patient => patient.Id == id);
        //     .AsNoTracking().FirstOrDefaultAsync(p => p.id == id);
        return found;
    }
    public async Task<Patient> CreateAsync(Patient patient)
    {
        EntityEntry<Patient> newPatient = await context.Patients.AddAsync(patient);
        await context.SaveChangesAsync();
        return newPatient.Entity;
    }
}