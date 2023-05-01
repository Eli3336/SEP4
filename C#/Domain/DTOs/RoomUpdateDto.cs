using System.Diagnostics;

namespace Domain.DTOs;

public class RoomUpdateDto
{
    public int Id { get; set; }
    public int? Capacity { get; set; }
    public string? Availability { get; set; }
    
    public RoomUpdateDto(){}

    public RoomUpdateDto(int id, int? capacity, string? availability)
    {
        Id = id;
        Capacity = capacity;
        Availability = availability;
    }
}