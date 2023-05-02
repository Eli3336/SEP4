using System.Net.WebSockets;
using System.Text;
using Application.DaoInterfaces;
using Domain.Models;
using EfcDataAccess;
using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Implementations;
using Services.Interfaces;
using WebAPI.IoTGate.Interface;
using WebAPI.IoTGate.Stream;

namespace WebAPI.IoTGate;

public class LoriotClient : IWebClient
{
    private IRecordService _recordService= new RecordService(new RecordDAO(new DBContext()));
    private RoomEfcDao _boxDao = new RoomEfcDao(new DBContext);
    private ILoriotDao _loriotDao = new LoriotDao(new DBContext());
    private ClientWebSocket _clientWebSocket;
    
    
    
    private readonly string _uriAddress = "ws://iotnet.cibicom.dk/app?token=vnoVQgAAABFpb3RuZXQudGVyYWNvbS5kawcinBwAkIjcdx98hF2KBE8=";
    private string _eui = String.Empty;

    public LoriotClient()
    {
        _clientWebSocket = new ClientWebSocket();
        ConnectClientAsync();
    }
    
    private List<SensorValue?> ReceivedData(string receivedJson)
    {
        var details = JObject.Parse(receivedJson);
        char[] array = details["data"].Value<String>().ToCharArray();
        float humidity = Convert.ToInt16(array[0].ToString()+array[1].ToString()+array[2].ToString()+array[3].ToString(),16);
        float temperature= Convert.ToInt16(array[4].ToString()+array[5].ToString()+array[6].ToString()+array[7].ToString(),16);
        float co2 = Convert.ToInt16(array[8].ToString()+array[9].ToString()+array[10].ToString()+array[11].ToString(),16);
        Console.WriteLine(humidity + " " + temperature + " " + co2);

        List<SensorValue?> sensorValues = new List<SensorValue?>();
        
        SensorValue record = new SensorValue()
        {
            Value = temperature,
            TimeStamp = DateTime.UtcNow
        };
        SensorValue record2 = new SensorValue()
        {
            Value = humidity,
            TimeStamp = DateTime.UtcNow
        };
        SensorValue record3 = new SensorValue()
        {
            Value = co2,
            TimeStamp = DateTime.UtcNow
        };
        
        sensorValues.Add(record);
        sensorValues.Add(record2);
        sensorValues.Add(record3);
        
        return sensorValues;
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
            ICollection<Room> rooms = await _boxDao.GetAllRoomsAsync();
            foreach (var room in rooms)
            {
                _eui = room.Sensors.id;
                
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
                SensorValue? getRecord = ReceivedData(strResult);
                await _loriotDao.CreateAsync(getRecord, _eui);
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