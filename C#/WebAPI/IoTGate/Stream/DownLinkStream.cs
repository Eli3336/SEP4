namespace WebAPI.IoTGate.Stream;

public class DownLinkStream
{
    public string cmd { get; set; }
    public string EUI { get; set; }
    public int port { get; set; }
    
    public bool confirmed { get; set; }
    public string data { get; set; }
}