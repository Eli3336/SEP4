namespace Domain.DTOs;

public class SensorValueDto
{
    

    public double value { get; set; }
    public DateTime timeStamp { get; set; }
    
    public SensorValueDto(double value, DateTime timeStamp)
        {
            this.value = value;
            this.timeStamp = timeStamp;
        }

    public SensorValueDto()
    {
        throw new NotImplementedException();
    }
}