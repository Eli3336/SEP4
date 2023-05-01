using Domain.DTOs;
using Domain.Models;

namespace WebAPI.IoTGate.Loriot;

public interface ILoriotTasks
{
    Task<SensorValue> AddTemperature(SensorValueDto temperatureValue, string eui);
    Task<SensorValue> AddHumidity(SensorValueDto humidityValue, string eui);
    Task<SensorValue> AddCo2(SensorValueDto co2Value, string eui);
}