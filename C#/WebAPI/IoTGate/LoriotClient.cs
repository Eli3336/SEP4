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
    private  SensorValueEfcDao _sensorValueEfcDao= new SensorValueEfcDao(new HospitalContext());
    private  SensorEfcDao _sensorEfcDao= new SensorEfcDao(new HospitalContext());
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
        float flag = Convert.ToInt16(array[12].ToString()+array[13].ToString()+array[14].ToString()+array[15].ToString(),16);

        
        Console.WriteLine(humidity + " " + temperature + " " + co2 +" " + flag);
        
        humidity = humidity / 10;
        temperature = temperature / 10;
        
        List<SensorValueDto> sensorValues = new List<SensorValueDto>();

        switch (flag)
        {
            case 1: 
                temperature = -1000;
                break;
            case 2:
                humidity = -1000;
                break;
            case 3:
                co2 = -1000;
                break;
            default:
                break;

        }
        SensorValueDto record = new SensorValueDto()
        {
            
            value = Math.Round(temperature, 1),
            timeStamp = DateTime.UtcNow
        };
      SensorValueDto record2 = new SensorValueDto()
        {
            value =  Math.Round(humidity,1),
            timeStamp = DateTime.UtcNow
        };
        SensorValueDto record3 = new SensorValueDto()
        {
            value = co2,
            timeStamp = DateTime.UtcNow
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
            IEnumerable<Room> rooms = await _roomEfcDao.GetAllRoomsAsync();
            Console.WriteLine(rooms);
            foreach (var room in rooms)
            {
                _eui = "0004A30B00ED3752";
                
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
                await _sensorValueEfcDao.CreateAsync(getRecord[0], 1);
               await _sensorValueEfcDao.CreateAsync(getRecord[1], 2);
               await _sensorValueEfcDao.CreateAsync(getRecord[2], 3);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

public async Task WSSendData()
{
    try
    {
        _eui = "0004A30B00ED3752";

        Console.WriteLine("WS-CLIENT--------->START");

        // Retrieve sensor data including breakpoints from the database for a specific sensor (e.g., sensor with ID 1)
        Sensor sensorTemp = await _sensorEfcDao.GetById(1);
        Sensor sensorHumidity = await _sensorEfcDao.GetById(2);
        Sensor sensorCo2 = await _sensorEfcDao.GetById(3);

        // Create a DownLinkStream object
        DownLinkStream downLinkStream = new DownLinkStream
        {
            cmd = "tx",
            EUI = _eui,
            port = 2,
            data = ""
        };

        // Convert humidity breakpoints to hex string
        string humidityHex = ConvertBreakpointsToHex(sensorHumidity.DownBreakpoint, sensorHumidity.UpBreakpoint);
        downLinkStream.data += humidityHex;

        // Convert temperature breakpoints to hex string
        string temperatureHex = ConvertBreakpointsToHex(sensorTemp.DownBreakpoint, sensorTemp.UpBreakpoint);
        downLinkStream.data += temperatureHex;

        // Convert CO2 breakpoints to hex string
        string co2Hex = ConvertBreakpointsToHex(sensorCo2.DownBreakpoint, sensorCo2.UpBreakpoint);
        downLinkStream.data += co2Hex;

        // Convert the DownLinkStream object to a JSON object
        string payloadJson = JsonConvert.SerializeObject(downLinkStream);

        // Convert the payload to a byte array
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadJson);

        // Connect to the WebSocket server if not connected
        if (_clientWebSocket.State != WebSocketState.Open)
            await ConnectClientAsync();

        // Send the payload as a binary message to the server
        await _clientWebSocket.SendAsync(new ArraySegment<byte>(payloadBytes), WebSocketMessageType.Binary, true, CancellationToken.None);

        // If needed, you can handle the server's response here
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

private string ConvertBreakpointsToHex(double? down, double? up)
{
    string hex = "";

    if (down.HasValue && up.HasValue)
    {
        byte[] downBytes = BitConverter.GetBytes(down.Value);
        byte[] upBytes = BitConverter.GetBytes(up.Value);

        hex += BitConverter.ToString(downBytes).Replace("-", "");
        hex += BitConverter.ToString(upBytes).Replace("-", "");
    }

    return hex;
}



}