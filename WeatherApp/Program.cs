using System.Net;
using Newtonsoft.Json;
using WeatherApp.BotHandlers;
using WeatherApp.WeatherInfo;
using WeatherApp.MySql;
using WeatherApp.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;


namespace WeatherApp
{
    class Program
    {
        private static string TlgToken { get; set; } = "5426302661:AAG8XZ4DYwuTTlxh7yMDbQm0dFv7hLZwaEQ";
        private static TelegramBotClient bot;


        static void Main(string[] args)
        {
            bot = new TelegramBotClient(TlgToken);

            Console.WriteLine($"Запущен бот "+bot.GetMeAsync().Result.FirstName);

            using var cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(
                Handlers.HandleUpdateAsync,
                Handlers.HandleErrorAsync,
                receiverOptions,
                cancellationToken : cts.Token
            );

            Console.ReadLine();
            cts.Cancel();
        }
    }
}

