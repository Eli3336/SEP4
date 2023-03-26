using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SensorLogic : ISensorLogic
{
    private readonly ISensorDao sensorDao;

    public SensorLogic(ISensorDao sensorDao)
    {
        this.sensorDao = sensorDao;
    }

    public async Task<Sensor> CreateAsync(SensorCreationDto dto)
    {
        Sensor toCreate = new Sensor()
        {
            Type = dto.Type,
            Values = dto.Values
        };
    
        Sensor created = await sensorDao.CreateAsync(toCreate);
    
        return created;
    }

    public Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId)
    {
        return sensorDao.GetSensorsValuesAsync(roomId);
    }
}