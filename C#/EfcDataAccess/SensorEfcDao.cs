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
        List<SensorValue> sensorValues = new List<SensorValue>();
        
        Room? roomToGet = await context.Rooms.Include(room => room.Sensors)
            .SingleOrDefaultAsync(room => room.Id == roomId);
        if (roomToGet != null)
        {
            int sensorId1 = roomToGet.Sensors[0].Id;
            int sensorId2 = roomToGet.Sensors[1].Id;
            int sensorId3 = roomToGet.Sensors[2].Id;


            Sensor? sensor1 = await context.Sensors.Include(sensor => sensor.Values)
                .SingleOrDefaultAsync(sensor => sensor.Id == sensorId1);
            Sensor? sensor2 = await context.Sensors.Include(sensor => sensor.Values)
                .SingleOrDefaultAsync(sensor => sensor.Id == sensorId2);
            Sensor? sensor3 = await context.Sensors.Include(sensor => sensor.Values)
                .SingleOrDefaultAsync(sensor => sensor.Id == sensorId3);

            if (sensor1 != null && sensor2 != null && sensor3 != null)
            {
                sensorValues.Add(sensor1.Values[sensor1.Values.Count - 1]);
                sensorValues.Add(sensor2.Values[sensor2.Values.Count - 1]);
                sensorValues.Add(sensor3.Values[sensor3.Values.Count - 1]);
            }

            else throw new Exception("Sensors are null");


        }

        return sensorValues;


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
}

