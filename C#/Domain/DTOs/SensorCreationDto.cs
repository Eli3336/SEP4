using Domain.Models;

namespace Domain.DTOs;

public class SensorCreationDto
{
    public string Type { get; }
    public List<SensorValue> Values { get; }

    public SensorCreationDto(string type)
    {
        Type = type;
        Values = new List<SensorValue>();
        Values.Add(new SensorValue(20.4));
        Values.Add(new SensorValue(35));
        Values.Add(new SensorValue(0.2));
    }
}