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

    public async Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int? roomId)
    {
        return await sensorDao.GetSensorsValuesAsync(roomId);
    }

    public async Task<IEnumerable<SensorValue>> GetLogOfSensorValuesAsync(int? sensorId)
    {
        return await sensorDao.GetLogOfSensorValuesAsync(sensorId);
    }
    
    public async Task SensorUpdateAsync(int id, double upbreakpoint, double downbreakpoint)
    {
        Sensor? existing = await sensorDao.GetByIdToUpdateAsync(id);
        if (existing == null)
        {
            throw new Exception($"Sensor with ID {id} not found!");
        }
        
       SensorUpdateDto dto = new SensorUpdateDto(id, upbreakpoint, downbreakpoint);

        double? upToUse = dto.UpBreakPoint ?? existing.UpBreakpoint;
        double? downToUse = dto.DownBreakPoint ?? existing.DownBreakpoint;
        
        
        Sensor updated = new (upToUse, downToUse)
        {
            Id = existing.Id,
            Type = existing.Type,
            Values = existing.Values
        };
        ValidateSensorUpdate(updated);
        await sensorDao.SensorUpdateAsync(updated);
    }
    
    private void ValidateSensorUpdate(Sensor sensor)
    {
        if (sensor.Type.Equals("Temperature") && (sensor.DownBreakpoint<-50 || sensor.UpBreakpoint > 50 || sensor.UpBreakpoint < sensor.DownBreakpoint))
        {
            throw new Exception("The Temperature is invalid!");
        }
        if (sensor.Type.Equals("Humidity") && (sensor.DownBreakpoint<10 || sensor.UpBreakpoint > 80 || sensor.UpBreakpoint < sensor.DownBreakpoint))
        {
            throw new Exception("The humidity is invalid!");
        }
        if (sensor.Type.Equals("CO2") && (sensor.DownBreakpoint<400 || sensor.UpBreakpoint > 2100 || sensor.UpBreakpoint < sensor.DownBreakpoint))
        {
            throw new Exception("The CO2 level is invalid!");
        }
    }

}