namespace WebAPI.IoTGate.Model;

public class IoTStruct
{
    public string Data { get; set; }
    public string EUI { get; set; }
    public string Cmd { get; set; }
    public int Port { get; set; }
    public long Ts { get; set; }
    public List<IoTStruct> Cache { get; set; }

    public override string ToString()
    {
        if (Cmd.Equals("cq"))
        {
            foreach (var j in Cache)
            {
                Console.WriteLine(j);
            }
        }
        return $"cmd: {Cmd}\nEUI: {EUI}\nport: {Port}\nts: {Ts}\ndata: {Data}";
    }
}