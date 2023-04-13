using Domain.Models;

namespace Domain.DTOs;

public class RoomCreationDto
{
    public string Name { get; }
    public int Capacity { get; }
    public string Availability { get; }
    public List<Sensor> Sensors { get; }

    public RoomCreationDto(string name, int capacity, string availability, List<Sensor> sensors)
    {
        Name = name;
        Capacity = capacity;
        Availability = availability;
        Sensors = sensors;
    }
}