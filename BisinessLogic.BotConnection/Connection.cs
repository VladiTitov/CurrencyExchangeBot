using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using BusinessLogic.MenuStucture;

namespace BusinessLogic.BotConnection
{
    public class Connection
    {
        private readonly ITelegramBotClient _botClient;

        public Connection(string token)
        {
            _botClient = new TelegramBotClient(token) { Timeout = TimeSpan.FromSeconds(10) };
        }

        [Obsolete]
        public void Start()
        {
            var bot = _botClient.GetMeAsync().Result;

            Console.WriteLine($"{bot.Id} and {bot.FirstName} started");

            _botClient.OnMessage += BotClient_OnMessage;
            _botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            _botClient.StartReceiving();
        }

        [Obsolete]
        private void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e) =>
            new MenuEvent(e.CallbackQuery.Message.Chat, _botClient).Start(e?.CallbackQuery?.Data);

        [Obsolete]
        private void BotClient_OnMessage(object sender, MessageEventArgs e) =>
            new MenuEvent(e.Message.Chat, _botClient).Start(e?.Message?.Text);
    }
}
