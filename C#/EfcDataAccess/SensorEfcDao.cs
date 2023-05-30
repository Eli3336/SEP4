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
        Sensor? sensor = await context.Sensors.Include(sensor => sensor.Values)
            .SingleOrDefaultAsync(sensor => sensor.Id == id);
        return sensor;    
    }

    public async Task<IEnumerable<SensorValue>> GetLogOfSensorValuesAsync(int? sensorId)
    {
        Sensor? sensor = await context.Sensors.Include(sensor => sensor.Values)
            .SingleOrDefaultAsync(sensor => sensor.Id == sensorId);
        if (sensor != null)
        {
            return sensor.Values.AsEnumerable();
        }
        else throw new Exception("Sensor with given Id not found.");
    }
    
    public async Task SensorUpdateAsync(Sensor sensor)
    {
        context.Sensors.Update(sensor);
        await context.SaveChangesAsync();

    }
    
    public async Task<Sensor?> GetByIdToUpdateAsync(int? id)
    {
        Sensor? found = await context.Sensors
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        return found;
    }
}

