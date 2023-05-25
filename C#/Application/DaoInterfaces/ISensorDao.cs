using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISensorDao
{
    Task<Sensor> CreateAsync(Sensor sensor);
    Task<Sensor?> GetById(int id);
    Task<IEnumerable<SensorValue>> GetLogOfSensorValuesAsync(int? sensorId);
    Task SensorUpdateAsync(Sensor sensor);
    Task<Sensor?> GetByIdToUpdateAsync(int? id);
}