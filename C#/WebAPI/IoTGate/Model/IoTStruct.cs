


namespace WebAPI.IoTGate.Model;

public class IoTStruct
{
    public string data { get; set; }
    public string EUI { get; set; }
    
    public string cmd { get; set; }
        
  
    public int port { get; set; }
    
    public long ts { get; set; }
    
    public List<IoTStruct> cache { get; set; }

    public override string ToString()
    {
        if (cmd.Equals("cq"))
        {
            foreach (var j in cache)
            {
                Console.WriteLine(j);
            }
        }
        return $"cmd: {cmd}\nEUI: {EUI}\nport: {port}\nts: {ts}\ndata: {data}";
    }
}