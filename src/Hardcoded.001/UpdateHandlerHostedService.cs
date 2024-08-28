using System.Threading.Channels;
using Telegram.Bot.Types;

namespace Hardcoded._001;

public sealed class UpdateHandlerHostedService : BackgroundService
{
    private readonly Channel<Update> _channel;

    public UpdateHandlerHostedService(Channel<Update> channel)
    {
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (_channel.Reader.TryRead(out var update))
            {
                await ExpensiveOperation();
            }
        }
    }
    
    private async Task ExpensiveOperation()
    {
        await Task.Delay(5000);
        Console.WriteLine("Hard work done!");
    }
}