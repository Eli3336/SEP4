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
                   
                    var measurement = _processor.CreateValue(data);
                    await _loriotTasks.AddMeasurement(measurement, data.EUI);
                    break;
                }
            }
        }
    }
}