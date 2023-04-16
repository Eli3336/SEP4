using Domain.Models;

namespace WebAPI.IoTGate.Loriot;

public interface ILoriotTasks
{
    Task AddMeasurement(SensorValue sensorValue, string eui);
}