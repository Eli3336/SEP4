namespace WebAPI.IoTGate.Model;

public class DownMessage
{
    public string data { get; set; }
    public int port { get; set; }
   
    public string eui { get; set; }
    
    public bool confirmation { get; set; }
  
    public string cmd { get; set; }

    public override string ToString()
    {
        return
            $"Down Message => CMD = {cmd}, EUI = {eui}, PORT = {port}, CONFIRMED = {confirmation}, DATA = {data}";
    }
}
