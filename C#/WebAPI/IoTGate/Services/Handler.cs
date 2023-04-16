

using Domain.Models;
using WebAPI.IoTGate.Model;

namespace WebAPI.IoTGate.Services
{
    public class MessageProcessor
    {

       /* public Measurement CreateMeasurement(IoTStruct data)
        {
            var measurement = new Measurement();
            measurement.Temperature = CreateTemperature(data).TemperatureInDegrees;
            measurement.Humidity = CreateHumidity(data).RelativeHumidity;
            measurement.CO2 = CreateCo2(data).CO2Level;
            measurement.Light = CreateLight(data).LightLevel;
            measurement.TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(data.ts).DateTime;
            return measurement;
        }
        */
       
        public SensorValue CreateValue(IoTStruct message)
        {
            String hexString = message.data;
            
            //Byte[0]
            int dec = int.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            
            //Byte[1]
            int point = int.Parse(hexString.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
            
            Console.WriteLine($"Decimal: {dec} Point: {point}");
            double number = (dec + (point / 100.0));
            Console.WriteLine($"Number: {number}");
            
            return new()
            {
                value = number,
                timeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime,
                EUI = message.EUI
            }; 
        }

       
        
    }
}