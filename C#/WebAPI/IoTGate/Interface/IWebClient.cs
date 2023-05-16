namespace WebAPI.IoTGate.Interface
{
    public interface IWebClient
    {
        Task WSGetData();
        Task WSSendData();
    }
}

