using System.Threading.Channels;
using Hardcoded._001;
using Microsoft.AspNetCore.Http.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration["TELEGRAM_BOT_TOKEN"]!;
var baseUrl = builder.Configuration["BASE_URL"]!;

builder.Services.ConfigureTelegramBot<JsonOptions>(opt => opt.SerializerOptions);

builder.Services.AddHttpClient("bot")
    .RemoveAllLoggers()
    .AddTypedClient(httpClient => new TelegramBotClient(token, httpClient));

builder.Services.AddSingleton(Channel.CreateUnbounded<Update>());
builder.Services.AddSingleton<IHostedService, UpdateHandlerHostedService>();
builder.Services.AddSingleton<IHostedService, UpdateHandlerHostedService>();
builder.Services.AddSingleton<IHostedService, UpdateHandlerHostedService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var bot = app.Services.GetRequiredService<TelegramBotClient>();
    await bot.SetWebhookAsync($"{baseUrl}/bot");
}

app.MapPost("/bot", async (Channel<Update> channel, Update update) =>
{
    if (update.Message is not null)
    {
        Console.WriteLine("Update received! {0}", update.Id);
        await channel.Writer.WriteAsync(update);
    }
});

app.Run();