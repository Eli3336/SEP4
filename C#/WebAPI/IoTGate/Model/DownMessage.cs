namespace WebAPI.IoTGate.Model;

public class DownMessage
{
    public string Data { get; set; }
    public int Port { get; set; }
    public string Eui { get; set; }
    public bool Confirmation { get; set; }
    public string Cmd { get; set; }

    public override string ToString()
    {
        return
            $"Down Message => CMD = {Cmd}, EUI = {Eui}, PORT = {Port}, CONFIRMED = {Confirmation}, DATA = {Data}";
    }
}
