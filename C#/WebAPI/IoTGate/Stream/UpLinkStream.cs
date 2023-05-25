namespace WebAPI.IoTGate.Stream;

public class UpLinkStream
{
    public string cmd { get; set; }
    public string EUI { get; set; }
    public long ts { get; set; }
    public bool ack { get; set; }
    public int fcnt { get; set; }
    public int port { get; set; }
    public string data { get; set; }
}