using System.Collections;
using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class SensorFileDao : ISensorDao
{
    private readonly FileContext context;

    public SensorFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<Sensor> CreateAsync(Sensor sensor)
    {
        int sensorId = 1;
        if (context.Sensors.Any())
        {
            sensorId = context.Sensors.Max(s => s.Id);
            sensorId++;
        }

        sensor.Id = sensorId;

        context.Sensors.Add(sensor);
        context.SaveChanges();

        return Task.FromResult(sensor);
    }
    public Task<Sensor?> GetById(int id)
    {
        Sensor? existing = context.Sensors.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(existing);
    }
    public Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId)
    {
        Room? existing = context.Rooms.FirstOrDefault(r => r.Id == roomId);
        List<SensorValue> values = new List<SensorValue>();
        List<Sensor> sensors = existing.Sensors;
        for (int i = 0; i < sensors.Count; i++)
        {
            values.Add(sensors[i].Values[sensors[i].Values.Count-1]);
        }
        IEnumerable<SensorValue> result = values.AsEnumerable();
        return Task.FromResult(result);
    }
}