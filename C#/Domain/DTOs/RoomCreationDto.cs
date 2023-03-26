using Domain.Models;

namespace Domain.DTOs;

public class RoomCreationDto
{
    public string Name { get;}

    public List<Sensor> Sensors { get; }

    public RoomCreationDto(string name, List<Sensor> sensors)
    {
        Name = name;
        Sensors = sensors;
    }
}