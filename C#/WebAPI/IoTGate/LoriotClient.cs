using System.Net.WebSockets;
using System.Text;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.IoTGate.Interface;
using WebAPI.IoTGate.Stream;

namespace WebAPI.IoTGate;

public class LoriotClient : IWebClient
{
    private readonly RoomEfcDao _roomEfcDao = new RoomEfcDao(new HospitalContext());
    private  SensorValueEfcDao _sensorEfcDao= new SensorValueEfcDao(new HospitalContext());
    private readonly ClientWebSocket _clientWebSocket;
    
    
    
    private readonly string _uriAddress = "wss://iotnet.cibicom.dk/app?token=vnoVQgAAABFpb3RuZXQudGVyYWNvbS5kawcinBwAkIjcdx98hF2KBE8=";
    private string _eui = String.Empty;

    public LoriotClient()
    {
        _clientWebSocket = new ClientWebSocket();
        ConnectClientAsync();
    }
    
    private List<SensorValueDto> ReceivedData(string receivedJson)
    {
        
        var details = JObject.Parse(receivedJson);
        char[] array = details["data"].Value<String>().ToCharArray();
        float humidity = Convert.ToInt16(array[0].ToString()+array[1].ToString()+array[2].ToString()+array[3].ToString(),16);
        float temperature= Convert.ToInt16(array[4].ToString()+array[5].ToString()+array[6].ToString()+array[7].ToString(),16);
        float co2 = Convert.ToInt16(array[8].ToString()+array[9].ToString()+array[10].ToString()+array[11].ToString(),16);
        Console.WriteLine(humidity + " " + temperature + " " + co2);

        List<SensorValueDto> sensorValues = new List<SensorValueDto>();
        
        SensorValueDto record = new SensorValueDto()
        {
            value = 23,
            timeStamp = DateTime.UtcNow
        };
       /* SensorValue record2 = new SensorValue()
        {
            Value = humidity,
            TimeStamp = DateTime.UtcNow
        };
        SensorValue record3 = new SensorValue()
        {
            Value = co2,
            TimeStamp = DateTime.UtcNow
        };
        */
        sensorValues.Add(record);
        //sensorValues.Add(record2);
       // sensorValues.Add(record3);
        
        return sensorValues;
    }
    
    
    private async Task ConnectClientAsync()
    {
        try
        {
            Console.WriteLine("i got here");
            await _clientWebSocket.ConnectAsync(new Uri(_uriAddress), CancellationToken.None);
        }
        catch (Exception e)
        {
            Console.WriteLine("i crashed at connect");

            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    public async Task WSGetData()
    {
        try
        {
            IEnumerable<Room> rooms = await _roomEfcDao.GetAllRoomsAsync();
            Console.WriteLine(rooms);
            foreach (var room in rooms)
            {
                _eui = "eeeds";
                
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
               List<SensorValueDto> getRecord = ReceivedData(strResult);
                await _sensorEfcDao.CreateAsync(getRecord[0], 1);
               // await _sensorEfcDao.CreateAsync(getRecord[1], 2);
               // await _sensorEfcDao.CreateAsync(getRecord[2], 3);
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