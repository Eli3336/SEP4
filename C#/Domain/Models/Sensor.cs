namespace Domain.Models;

public class Sensor
{
    public int Id { get; set; }
    public string Type { get; set; }
    public double? UpBreakpoint { get; set; }
    public double? DownBreakpoint { get; set; }
    public List<SensorValue> Values { get; set; }

    public Sensor() {}

    public Sensor(string type, List<SensorValue> values)
    {
        Type = type;
        Values = values;
        UpBreakpoint = null;
        DownBreakpoint = null;
    }

    public Sensor(string type, double? upBreakpoint, double? downBreakpoint, List<SensorValue> values)
    {
        Type = type;
        UpBreakpoint = upBreakpoint;
        DownBreakpoint = downBreakpoint;
        Values = values;
    }
}