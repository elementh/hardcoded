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
        throw new NotImplementedException();
    }
}