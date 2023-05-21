namespace Domain.DTOs;

public class SensorUpdateDto
{
    public int Id { get; set; }
    public double? UpBreakPoint { get; set; }
    public double? DownBreakPoint { get; set; }


    public SensorUpdateDto(){}

    public SensorUpdateDto(int id,  double? upBreakPoint, double? downBreakPoint)
    {
        Id = id;
        UpBreakPoint = upBreakPoint;
        DownBreakPoint = downBreakPoint;

    }
}