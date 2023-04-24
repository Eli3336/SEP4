using Domain.DTOs;
using Domain.Models;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.IoTGate.Loriot;

public class LoriotImpl: ILoriotTasks
{
    // public async Task AddMeasurement(SensorValue sensorValue, string eui)
    // {
    //     await using var database = new HospitalContext();
    //         //Database(); //this is a class that they created i need to change it to something that will deal with adding values, need to know where the values should be added
    //
    //     await database.SensorValue.AddAsync(sensorValue);
    //     await database.SaveChangesAsync();
    // }
    private readonly ILoriotDao loriotDao;
    
    public LoriotImpl(ILoriotDao loriotDao)
    {
        this.loriotDao = loriotDao;
    }

    public LoriotImpl()
    {
        
    }

    public async Task<SensorValue> AddTemperature(SensorValueDto tempValue, string eui) 
    {
        
        SensorValue toCreate = new SensorValue()
        {
            value = tempValue.value,
            timeStamp = tempValue.timeStamp
        };
    
        SensorValue created = await loriotDao.CreateAsync(toCreate, 1);
        return created;
    }

    public async Task<SensorValue>  AddHumidity(SensorValueDto humidityValue, string eui)
    {
       
        SensorValue toCreate = new SensorValue()
        {
            value = humidityValue.value,
            timeStamp = humidityValue.timeStamp
        };
    
        SensorValue created = await loriotDao.CreateAsync(toCreate, 2);
        return created;
    }

    public async Task<SensorValue>  AddCo2(SensorValueDto co2Value, string eui)
    {
       
        SensorValue toCreate = new SensorValue()
        {
            value = co2Value.value,
            timeStamp = co2Value.timeStamp
        };
    
        SensorValue created = await loriotDao.CreateAsync(toCreate, 3);
        return created;
    }
}