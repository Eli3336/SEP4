using Domain.DTOs;
using WebAPI.IoTGate.Model;

namespace WebAPI.IoTGate.Services;
public class MessageProcessor
{
    public SensorValueDto CreateTemperature(IoTStruct message)
    {
        String hexString = message.Data;
            
        //Byte[0]
        int dec = int.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            
        //Byte[1]
        int point = int.Parse(hexString.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
        
        Console.WriteLine($"Decimal: {dec} Point: {point}");
        double number = (dec + (point / 100.0));
        Console.WriteLine($"Number: {number}");
            
        return new ()
        { 
            value = number, 
            timeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.Ts).DateTime,
        }; 
    }
        
    public SensorValueDto CreateHumidity(IoTStruct message)
    {
        String hexString = message.Data;
        //Byte[2]
        int humidity = int.Parse(hexString.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
        return new SensorValueDto()
        {
            value = humidity,
            timeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.Ts).DateTime,
        }; 
    }
    
    public SensorValueDto CreateCo2(IoTStruct message)
    {
        String hexString = message.Data;
        int co2Level = int.Parse(hexString.Substring(6,4), System.Globalization.NumberStyles.HexNumber);
        
        return new()
        {
            value = co2Level,
            timeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.Ts).DateTime,
        }; 
    }
}