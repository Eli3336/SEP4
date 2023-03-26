using System.Collections;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ISensorLogic
{
    Task<Sensor> CreateAsync(SensorCreationDto dto);
    Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId);
}