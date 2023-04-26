using System.Diagnostics;

namespace Domain.DTOs;

public class RoomUpdateDto
{
    public int Id { get; set; }
    public int? Capacity { get; set; }
    public string? Status { get; set; }
    
    public RoomUpdateDto(){}

    public RoomUpdateDto(int id, int? capacity, string? status)
    {
        this.Id = id;
        this.Capacity = capacity;
        this.Status = status;
    }
}