using System.Net.WebSockets;
using System.Text;
using EfcDataAccess;
using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Implementations;
using Services.Interfaces;
using WebAPI.IoTGate.Interface;
using WebAPI.IoTGate.Stream;

namespace WebAPI.IoTGate;

public class RecordClient : IWebClient
{
    private IRecordService _recordService= new RecordService(new RecordDAO(new DBContext()));
    private IBoxDao _boxDao = new BoxDao(new DBContext());
    private ClientWebSocket _clientWebSocket;
    
    
    
    private readonly string _uriAddress = "wss://iotnet.cibicom.dk/app?token=vnoUeAAAABFpb3RuZXQudGVyYWNvbS5kawhxYha6idspsvrlQ4C7KWA=";
    private string _eui = String.Empty;

    public RecordClient()
    {
        _clientWebSocket = new ClientWebSocket();
        ConnectClientAsync();
    }
    
    private Record? ReceivedData(string receivedJson)
    {
        var details = JObject.Parse(receivedJson);
        char[] array = details["data"].Value<String>().ToCharArray();
        float humidity = Convert.ToInt16(array[0].ToString()+array[1].ToString()+array[2].ToString()+array[3].ToString(),16);
        float temperature= Convert.ToInt16(array[4].ToString()+array[5].ToString()+array[6].ToString()+array[7].ToString(),16);
        float co2 = Convert.ToInt16(array[8].ToString()+array[9].ToString()+array[10].ToString()+array[11].ToString(),16);
        Console.WriteLine(humidity + " " + temperature + " " + co2);

        Record record = new Record()
        {
            Humidity = humidity,
            Temperature = temperature,
            CO2 = co2,
            BoxId = _eui,
            Timestamp = DateTime.UtcNow
        };
        return record;
    }
    
    
    private async Task ConnectClientAsync()
    {
        try
        {
            await _clientWebSocket.ConnectAsync(new Uri(_uriAddress), CancellationToken.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task WSGetData()
    {
        try
        {
            ICollection<Box> boxes = await _boxDao.GetBoxesAsync();
            foreach (var box in boxes)
            {
                _eui = box.Id;
                
                Console.WriteLine("WS-CLIENT--------->START");
                DownLinkStream upLinkStream = new()
                {
                    cmd = "tx",
                    EUI = _eui,
                    port = 2,
                    data = "EFC"
                };
                string payloadJson = JsonConvert.SerializeObject(upLinkStream);
                //send
                await _clientWebSocket.SendAsync(Encoding.UTF8.GetBytes(payloadJson), WebSocketMessageType.Text, true, CancellationToken.None);
            
                Byte[] buffer = new byte[500];
                var x = await _clientWebSocket.ReceiveAsync(buffer,CancellationToken.None);
                var strResult = Encoding.UTF8.GetString(buffer);
            
                //get data and convert
                Record? getRecord = ReceivedData(strResult);
                await _recordService.AddRecordDataAsync(getRecord);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public Task WSSendData()
    {
        throw new NotImplementedException();
    }
}