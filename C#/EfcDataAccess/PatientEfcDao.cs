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
    public async Task DeleteAsync(Patient patient)
    {
        context.Patients.Remove(patient);
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
    
    public async Task<IEnumerable<Patient?>> GetAllPatientsAsync()
    {
        IQueryable<Patient> patients = context.Patients.AsQueryable();
        IEnumerable<Patient> result = await patients.ToListAsync();
        return result;
    }
}