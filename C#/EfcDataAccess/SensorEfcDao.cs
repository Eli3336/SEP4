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
        
        Room? roomToGet = await context.Rooms.FindAsync(roomId);

        List<SensorValue> sensorValues = new List<SensorValue>();

        foreach (Sensor sensor in roomToGet.Sensors)
        {
            sensorValues.Add(sensor.Values.LastOrDefault());
        }

        return sensorValues;

    }

}