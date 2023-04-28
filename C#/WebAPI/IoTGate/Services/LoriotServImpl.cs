using WebAPI.IoTGate.Loriot;
using WebAPI.IoTGate.Model;
using WebAPI.IoTGate.Services;


namespace WebAPI.Gateway.Service
{
    public class LoriotServImpl : ILoriotServ
    {
        private readonly ILoriotTasks _loriotTasks;
        private MessageProcessor _processor;
       
        
        public LoriotServImpl()
        {
            _loriotTasks = new LoriotImpl();
            _processor = new MessageProcessor();
        }



        public async Task HandleData(IoTStruct data)
        {
            switch (data.cmd)
            {
                case "rx":
                {
                   
                    var temperature = _processor.CreateTemperature(data);
                    var humidity = _processor.CreateHumidity(data);
                    var co2 = _processor.CreateCo2(data);
                    await _loriotTasks.AddTemperature(temperature, data.EUI);
                    await _loriotTasks.AddHumidity(humidity, data.EUI);
                    await _loriotTasks.AddCo2(co2, data.EUI);
                    break;
                }
            }
        }
    }
}