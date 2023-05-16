using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace EfcDataAccess;

public class SensorValueEfcDao : ILoriotDao
{
    private readonly HospitalContext context;

    public SensorValueEfcDao(HospitalContext context)
    {
        this.context = context;
    }

    public async Task<SensorValue> CreateAsync(SensorValueDto sensorValueDto, int id)
    {
        SensorValue? sensorValue = new SensorValue(sensorValueDto.value, sensorValueDto.timeStamp);
        
        EntityEntry<SensorValue> newSensorValue = await context.SensorValue.AddAsync(sensorValue);
        var sensor = await context.Sensors.Include(s => s.Values)
            .FirstAsync(p => p.Id.Equals(id));
        sensor.Values.Add(sensorValue);
        context.Update(sensor);
        
        await context.SaveChangesAsync();
        return newSensorValue.Entity;
    }
}