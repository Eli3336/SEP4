using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class SensorValue
{
    public int valueId;
    public double value { get; set; }
    public DateTime timeStamp { get; set; }
    

    public SensorValue()
    {
        this.value = 0;
        this.timeStamp = DateTime.Now;
        this.valueId = 0;
    }
    
    public SensorValue(double value)
    {
        this.value = value;
        this.timeStamp = DateTime.Now;
        this.valueId = 0;
    }

    public SensorValue(int valueId, double value, DateTime timeStamp)
    {
        this.value = value;
        this.timeStamp = timeStamp;
        this.valueId = valueId;
    }
}