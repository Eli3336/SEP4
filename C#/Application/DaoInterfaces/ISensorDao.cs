using System.Collections;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISensorDao
{
    Task<Sensor> CreateAsync(Sensor sensor);
    Task<Sensor?> GetById(int id);
    Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId);
}