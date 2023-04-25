using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI.IoTGate.Loriot;
namespace EfcDataAccess;

public class SensorValueEfcDao : ILoriotDao
{
    private readonly HospitalContext context;

    public SensorValueEfcDao(HospitalContext context)
    {
        this.context = context;
    }

    public async Task<SensorValue> CreateAsync(SensorValue sensorValue, int id)
    {
        EntityEntry<SensorValue> newSensorValue = await context.SensorValue.AddAsync(sensorValue);
        var sensor = await context.Sensors.Include(s => s.Values)
            .FirstAsync(p => p.Id.Equals(id));
        sensor.Values.Add(sensorValue);
        context.Update(sensor);
        
        await context.SaveChangesAsync();
        return newSensorValue.Entity;
    }
}