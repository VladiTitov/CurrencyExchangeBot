using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Models.Interfaces;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.BotConnection
{
    public class Connection
    {
        private readonly ITelegramBotClient _botClient;

        public Connection(string token) =>
            _botClient = new TelegramBotClient(token) {Timeout = TimeSpan.FromSeconds(10)};

        [Obsolete]
        public void Connect()
        {
            var bot = _botClient.GetMeAsync().Result;
            Console.WriteLine($"{bot.Id} and {bot.FirstName} started");

            _botClient.OnMessage += BotClient_OnMessage;
            _botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            _botClient.StartReceiving();
        }

        [Obsolete]
        private async void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var ResponseModel = GetResponseModel(e.CallbackQuery.Message, new CallbackQueryHandler(e));
            try
            {
                await SendMessage(ResponseModel, e.CallbackQuery.Message.Chat);
            }
            catch
            {

            }
            
        }

        [Obsolete]
        private async void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            var ResponseModel = GetResponseModel(e.Message, new MessageHandler(e));
            try
            {
                await SendMessage(ResponseModel, e.Message.Chat);
            }
            catch
            {

            }
        }

        private UserResponseModel GetResponseModel(Message msg, IHandler handler)
        {
            var menuEventHandler = new MenuEventHandler(msg);
            menuEventHandler.Delete += DeleteMessage;
            handler.Process(menuEventHandler);

            return new MenuStateService(msg).GetMenuState();
        }

        public Task SendMessage(UserResponseModel state, Chat chat)
        {
            string text = state.ResponseLabel;
            IReplyMarkup replyMarkup = state.ResponseReplyMarkup;
            return _botClient.SendTextMessageAsync(
                chatId: chat,
                text: text,
                ParseMode.Markdown,
                null,
                false,
                false,
                0,
                true,
                replyMarkup,
                CancellationToken.None);
        }

        public void DeleteMessage(Message msg)
        {
            _botClient.DeleteMessageAsync(
                chatId: msg.Chat,
                messageId: msg.MessageId);
        }
    }
}
