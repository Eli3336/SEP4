using Domain.Models;

namespace Domain.DTOs;

public class SensorCreationDto
{
    public string Type { get; set; }
    public List<SensorValue> Values { get; set; }

    public SensorCreationDto(string type)
    {
        Type = type;
        Values = new List<SensorValue>();
    }
    public SensorCreationDto() {}
}