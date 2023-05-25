using System.Diagnostics;

namespace Domain.DTOs;

public class RoomUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Capacity { get; set; }
    public string? Availability { get; set; }
    
    public RoomUpdateDto(){}

    public RoomUpdateDto(int id, string? name, int? capacity, string? availability)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        Availability = availability;
    }
}