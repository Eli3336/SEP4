using System.Collections;
using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class SensorEfcDao : ISensorDao
{

    private readonly HospitalContext context;

    public SensorEfcDao(HospitalContext context)
    {
        this.context = context;
    }
    
    public async Task<Sensor> CreateAsync(Sensor sensor)
    {
        EntityEntry<Sensor> newSensor = await context.Sensors.AddAsync(sensor);
        await context.SaveChangesAsync();
        return newSensor.Entity;
    }

    public async Task<Sensor?> GetById(int id)
    {
        Sensor? sensor = await context.Sensors.FindAsync(id);
        return sensor;    }

    public async Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId)
    {
        
         
         var result2 = context.Rooms
             .Include(r => r.Sensors)
             .ThenInclude(s => s.Values)
             .Where(r => r.Id == roomId) 
             .SelectMany(r => r.Sensors) 
             .SelectMany(s => s.Values) 
             .AsQueryable();
         
         IEnumerable<SensorValue> result = await result2.ToListAsync();

         return result;
    }

}