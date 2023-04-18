using WebAPI.IoTGate.Model;

namespace WebAPI.IoTGate.Services;

public interface ILoriotServ
{
    
    public Task HandleData(IoTStruct data);
}