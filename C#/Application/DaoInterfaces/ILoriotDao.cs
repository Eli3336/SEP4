using Domain.Models;

namespace Application.DaoInterfaces;

public interface ILoriotDao
{
    Task<SensorValue> CreateAsync(SensorValue sensorValue, int id);
}