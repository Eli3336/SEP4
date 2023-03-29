using System.Collections;

namespace Domain.Models;

public class Sensor
{
    public int Id { get; set; }
    public String Type { get; set; }
    public double? UpBreakpoint { get; set; }
    public double? DownBreakpoint { get; set; }
    public List<SensorValue> Values { get; set; }

    public Sensor() {}

    public Sensor(string type, List<SensorValue> values)
    {
        this.Type = type;
        this.Values = values;
        this.UpBreakpoint = null;
        this.DownBreakpoint = null;
    }

    public Sensor(string type, double? upBreakpoint, double? downBreakpoint, List<SensorValue> values)
    {
        this.Type = type;
        this.UpBreakpoint = upBreakpoint;
        this.DownBreakpoint = downBreakpoint;
        this.Values = values;
    }
}