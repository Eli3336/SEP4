using System.Diagnostics;

namespace Domain.DTOs;

public class RoomUpdateDto
{
    public int Id;
    public int? Capacity;
    public String? Status;
    
    public RoomUpdateDto(){}

    public RoomUpdateDto(int id, int? capacity, String? status)
    {
        this.Id = id;
        this.Capacity = capacity;
        this.Status = status;
    }
}