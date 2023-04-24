using Domain.Models;

namespace Domain.DTOs;

public class RoomCreationDto
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Availability { get; set; }
    public List<Sensor> Sensors { get; set; }

    public RoomCreationDto(string name, int capacity, string availability, List<Sensor> sensors)
    {
        Name = name;
        Capacity = capacity;
        Availability = availability;
        Sensors = sensors;
    }

    public RoomCreationDto()
    {
        
    }
}