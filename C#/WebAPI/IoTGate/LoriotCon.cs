using System.Text.Json;
using WebAPI.Gateway.Service;
using WebAPI.IoTGate.Model;
using WebAPI.IoTGate.Services;
using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace WebAPI.Gateway
{
    public sealed class LoriotClient
    {
        private static readonly Lazy<LoriotClient> lazy =
            new(() => new LoriotClient());
        
        private WebSocket _socket;

        private string Url =
            "wss://iotnet.cibicom.dk/app?token=vnoVQgAAABFpb3RuZXQudGVyYWNvbS5kawcinBwAkIjcdx98hF2KBE8=";
        
        

        private ILoriotServ _loriotServ;
        
        public static LoriotClient Instance => lazy.Value;

       
        private LoriotClient()
        {
            _loriotServ = new LoriotServImpl();
            _socket = new WebSocket(Url);
            _socket.OnOpen += OnOpen;
            _socket.OnMessage += OnMessage;
            _socket.OnError += OnError;
            _socket.OnClose += OnClose;
            _socket.Connect();
        }
        
        public void SendDownLinkMessage(string eui, int toOpen)
        {
            string data = toOpen == 1
                ? "01"
                : "00";
            
            var message = new DownMessage()
            {
                cmd = "tx",
                eui = eui,
                port = 1,
                confirmation = true,
                data = data
            };
            string json = JsonSerializer.Serialize(message);
            Console.WriteLine(json);
            _socket.Send(json);
        }

        public void GetCacheReadings()
        {
            var message = new IoTStruct()
            {
                cmd = "cq"
            };
            var json = JsonSerializer.Serialize(message);
            _socket.Send(json);
        }

        private void OnOpen(object? sender, EventArgs e)
        {
            Console.WriteLine($"GATEWAY CONTROLLER => CONNECTION ESTABLISHED...");
        }

       
        private void OnMessage(object? sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            var message = JsonSerializer.Deserialize<IoTStruct>(e.Data);
            Console.WriteLine($"GATEWAY CONTROLLER => {message}");
            _loriotServ.HandleData(message);
        }

        private void OnError(object? sender, ErrorEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => ERROR OCCURED: " + e.Message);
        }

        private void OnClose(object? sender, CloseEventArgs e)
        {
            Console.WriteLine("GATEWAY CONTROLLER => Connection closed: " + e.Code);
        }
    }
}