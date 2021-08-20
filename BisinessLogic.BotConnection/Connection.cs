using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Services;

namespace BusinessLogic.BotConnection
{
    public class Connection
    {
        private readonly ITelegramBotClient _botClient;
        private readonly MenuEventService _menuEventService;

        public Connection(string token)
        {
            _botClient = new TelegramBotClient(token) { Timeout = TimeSpan.FromSeconds(10) };
            _menuEventService = new MenuEventService(_botClient);
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
            _menuEventService.Process(new CallbackQueryHandler(e));

        [Obsolete]
        private void BotClient_OnMessage(object sender, MessageEventArgs e) => 
            _menuEventService.Process(new MessageHandler(e));
    }
}
