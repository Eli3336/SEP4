using WebAPI.IoTGate.Interface;

namespace WebAPI.IoTGate.Background;

public class WebClientBackgroundService : BackgroundService
{
    private IWebClient _webClient;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _webClient = new LoriotClient();
            while (!stoppingToken.IsCancellationRequested)
            {
                await _webClient.WSGetData();
                await Task.Delay(TimeSpan.FromMinutes(0.5), stoppingToken);
                await _webClient.WSSendData();
                await Task.Delay(TimeSpan.FromMinutes(0.5), stoppingToken);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
    }
}