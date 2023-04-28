using Domain.Models;

namespace WebAPI.IoTGate.Loriot;

public interface ILoriotDao
{
    Task<SensorValue> CreateAsync(SensorValue sensorValue, int id);
}