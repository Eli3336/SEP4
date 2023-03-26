using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<Room> Rooms
    {
        get
        {
            LoadData();
            return dataContainer!.Rooms;
        }
    }
    public ICollection<Sensor> Sensors
    {
        get
        {
            LoadData();
            return dataContainer!.Sensors;
        }
    }
    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new ()
            {
                Rooms = new List<Room>(),
                Sensors = new List<Sensor>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}