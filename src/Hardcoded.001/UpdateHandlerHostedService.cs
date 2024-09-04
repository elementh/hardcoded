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
            if (_channel.Reader.TryRead(out var update))
            {
                await ExpensiveOperation(update);
            }
        }
    }
    
    private async Task ExpensiveOperation(Update update)
    {
        Console.WriteLine("Starting hard work... {0}", update.Id);
        await Task.Delay(5000);
        Console.WriteLine("Hard work done! {0}", update.Id);
    }
}