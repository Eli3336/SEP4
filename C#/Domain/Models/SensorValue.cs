namespace Domain.Models;

public class SensorValue
{
    public int ValueId { get; set; }
    public double Value { get; set; }
    public DateTime TimeStamp { get; set; }
    

    public SensorValue()
    {
        Value = 0;
        TimeStamp = DateTime.Now;
        ValueId = 0;
    }
    
    public SensorValue(double value)
    {
        Value = value;
        TimeStamp = DateTime.Now;
        ValueId = 0;
    }

    public SensorValue(int valueId, double value, DateTime timeStamp)
    {
        Value = value;
        TimeStamp = timeStamp;
        ValueId = valueId;
    }
}