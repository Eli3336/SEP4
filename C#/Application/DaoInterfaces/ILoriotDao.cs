using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ILoriotDao
{
    Task<SensorValue> CreateAsync(SensorValueDto sensorValueDto, int id);
}