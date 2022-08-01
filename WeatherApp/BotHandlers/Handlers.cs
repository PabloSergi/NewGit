using System.Net;
using Newtonsoft.Json;
using WeatherApp.WeatherInfo;
using WeatherApp.MySql;
using WeatherApp.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace WeatherApp.BotHandlers
{
    public class Handlers
    {
        private static long ChatId { get; set; }
        private static string? MessageText { get; set; }
        private static long? ID { get; set; }
        private static string? FirstName { get; set; }
        private static string? LastName { get; set; }
        private static int Year { get; set; }
        private static int Month { get; set; }
        private static int Day { get; set; }
        private static int Hour { get; set; }
        private static int Minute { get; set; }
        private static int Second { get; set; }
        private static Message? SentMessage { get; set; }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type != UpdateType.Message) // Только сообщения
                return;
            if (update.Message!.Type != MessageType.Text) // Только текстовые сообщения
                return;

            ChatId = update.Message.Chat.Id;
            MessageText = update.Message.Text.ToLower();
            ID = update.Message.From.Id;
            FirstName = update.Message.From.FirstName;
            LastName = update.Message.From.LastName;
            Year = update.Message.Date.Year;
            Month = update.Message.Date.Month;
            Day = update.Message.Date.Day;
            Hour = update.Message.Date.Hour;
            Minute = update.Message.Date.Minute;
            Second = update.Message.Date.Second;

            Console.WriteLine($"Дата использования бота: {Day}.{Month}.{Year}. | {Hour}:{Minute}:{Second}.");
            Console.WriteLine($"Бот использован пользователем: {FirstName} {LastName} | ID {ID}.");

            if (DBCheck.CheckCity(MessageText))
            {
                (string weatherText, string weatherPhoto) = WeatherCheck.CheckWeather(MessageText);

                SentMessage = await botClient.SendTextMessageAsync(
                chatId: ChatId,
                text: weatherText);

                SentMessage = await botClient.SendPhotoAsync(
                chatId: ChatId,
                photo: weatherPhoto,
                cancellationToken: cancellationToken);
            }
            else if (MessageText == "/start")
            {
                SentMessage = await botClient.SendTextMessageAsync(
                chatId: ChatId,
                text: "Привет! Введите название города погоду в котором хотите узнать",
                cancellationToken: cancellationToken);
            }
            else
            {
                SentMessage = await botClient.SendTextMessageAsync(
                chatId: ChatId,
                text: "Я не знаю такого города. Возможно он появится в DLC, а пока попробуйте ввести название подругому.",
                cancellationToken: cancellationToken);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}

