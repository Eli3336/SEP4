namespace Domain.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Availability { get; set; }
    public List<Patient> Patients { get; set; }
    public List <Sensor> Sensors { get; set; }

    public Room()
    {
        Patients = new List<Patient>();
    }

    public Room(string name, int capacity, string availability, List<Sensor> sensors)
    {
        Name = name;
        Capacity = capacity;
        Availability = availability;
        Patients = new List<Patient>();
        Sensors = sensors;
    }

    public Room(int capacity, String availability)
    {
        this.Capacity = capacity;
        this.Availability = availability;
    }
}