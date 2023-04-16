namespace WebAPI.IoTGate.Loriot;

public interface ILoriotTasks
{
    Task AddMeasurement(Measurement measurement, string eui);
}