using BusinessLogic.MenuStucture;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BusinessLogic.BotConnection
{
    public class Connection
    {
        private readonly string _token;
        private readonly ITelegramBotClient _botClient;
        public TelegramUser _user;

        public Connection(string token)
        {
            _token = token;
            _botClient = new TelegramBotClient(_token) { Timeout = TimeSpan.FromSeconds(10) };
        }

        [Obsolete]
        public void Start()
        {
            var bot = _botClient.GetMeAsync().Result;

            Console.WriteLine($"{bot.Id} and {bot.FirstName} started");

            _botClient.OnMessage += BotClient_OnMessage;
            _botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            _botClient.StartReceiving();

            _user = new TelegramUser();
        }

        [Obsolete]
        private void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e) =>
            new MenuContainer().MenuStart(e.CallbackQuery.Message.Chat, _botClient, e?.CallbackQuery?.Data);

        [Obsolete]
        public void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            new MenuContainer().MenuStart(e.Message.Chat, _botClient, e?.Message?.Text);
        }            
    }
}
