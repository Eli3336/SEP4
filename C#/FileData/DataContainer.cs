using Domain.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<Room> Rooms { get; set; }
    public ICollection<Sensor> Sensors { get; set; }
}