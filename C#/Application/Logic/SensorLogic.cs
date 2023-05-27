using System.Security.AccessControl;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SensorLogic : ISensorLogic
{
    private readonly ISensorDao sensorDao;
    private readonly IRoomDao roomDao;

    public SensorLogic(ISensorDao sensorDao, IRoomDao roomDao)
    {
        this.sensorDao = sensorDao;
        this.roomDao = roomDao;
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

    public async Task<IEnumerable<SensorValue>> GetSensorsValuesAsync(int roomId)
    {
        List<SensorValue> sensorValues = new List<SensorValue>(); 
        Room? roomToGet = await roomDao.GetById(roomId);
            
        if (roomToGet != null && roomToGet.Sensors.Count>0)
        {
            int sensorId1 = roomToGet.Sensors[0].Id;
            int sensorId2 = roomToGet.Sensors[1].Id;
            int sensorId3 = roomToGet.Sensors[2].Id;

            Sensor? sensor1 = await sensorDao.GetById(sensorId1); 
            Sensor? sensor2 = await sensorDao.GetById(sensorId2);
            Sensor? sensor3 = await sensorDao.GetById(sensorId3);

            if (sensor1 != null && sensor2 != null && sensor3 != null)
            {
                sensorValues.Add(sensor1.Values[sensor1.Values.Count - 1]);
                sensorValues.Add(sensor2.Values[sensor2.Values.Count - 1]);
                sensorValues.Add(sensor3.Values[sensor3.Values.Count - 1]);
            }
            else throw new Exception("Sensors are null");
        }
        else
        {
            throw new Exception("Something went wrong. Either the room does not exist, or it doesn't have sensors!");
        }
        return sensorValues;
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
        if (sensor.Type.Equals("Humidity") && (sensor.DownBreakpoint<0 || sensor.UpBreakpoint > 100 || sensor.UpBreakpoint < sensor.DownBreakpoint))
        {
            throw new Exception("The humidity is invalid!");
        }
        if (sensor.Type.Equals("CO2") && (sensor.DownBreakpoint<400 || sensor.UpBreakpoint > 5000 || sensor.UpBreakpoint < sensor.DownBreakpoint))
        {
            throw new Exception("The CO2 level is invalid!");
        }
    }

}