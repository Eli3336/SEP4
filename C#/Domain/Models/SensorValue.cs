namespace Domain.Models;

public class SensorValue
{
    public double value { get; set; }
    public DateTime timeStamp { get; set; }

    public SensorValue()
    {
        this.value = 0;
        this.timeStamp = DateTime.Now;
    }
    
    public SensorValue(double value)
    {
        this.value = value;
        this.timeStamp = DateTime.Now;
    }

    public SensorValue(double value, DateTime timeStamp)
    {
        this.value = value;
        this.timeStamp = timeStamp;
    }
}