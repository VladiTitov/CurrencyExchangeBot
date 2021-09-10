using System;
using System.IO;
using System.Text;
using System.Threading;
using BusinessLogic.GeoParser.Models;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Models.Interfaces;
using BusinessLogic.MenuStucture.Services;
using BusinessLogic.Serilog;
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

        public Connection()
        {
            using (var sr = new StreamReader("../token.txt", Encoding.UTF8))
            {
                string token = sr.ReadLine();
                if (token != null)
                {
                    _botClient = new TelegramBotClient(token)
                    {
                        Timeout = TimeSpan.FromSeconds(10)
                    };
                }
                else
                {
                    LoggingService.AddEventToLog("Token is null");
                }
            }
        }

        [Obsolete("This property is obsolete")]
        public void Connect()
        {
            var bot = _botClient.GetMeAsync().Result;
            Console.WriteLine($"{bot.Id} and {bot.FirstName} started");

            _botClient.OnMessage += BotClient_OnMessage;
            _botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
            _botClient.StartReceiving();
        }

        [Obsolete ("This property is obsolete")]
        private  void BotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e) => 
            GetResponseModel(e.CallbackQuery.Message, new CallbackQueryHandler(e));

        [Obsolete("This property is obsolete")]
        private  void BotClient_OnMessage(object sender, MessageEventArgs e) => 
            GetResponseModel(e.Message, new MessageHandler(e));

        private void GetResponseModel(Message msg, IHandler handler)
        {
            try
            {
                var menuEventHandler = new MenuEventHandler(msg);

                menuEventHandler.DeleteMessage += DeleteMessage;
                menuEventHandler.SendLocation += SendLocation;
                menuEventHandler.SendMessage += SendMessage;

                handler.Process(menuEventHandler);
            }
            catch
            {
                LoggingService.AddEventToLog($"Cannot send message to user {msg.Chat.Id}");
            }
            
        }

        private void SendMessage(UserResponseModel state, Message message)
        {
            string text = state.ResponseLabel;
            IReplyMarkup replyMarkup = state.ResponseReplyMarkup;
            
            _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: text, ParseMode.Markdown,
                entities:null,
                disableWebPagePreview:false,
                disableNotification:false,
                replyToMessageId:0,
                allowSendingWithoutReply:true,
                replyMarkup,
                CancellationToken.None);
        }

        private void DeleteMessage(Message msg)
        {
            _botClient.DeleteMessageAsync(
                chatId: msg.Chat,
                messageId: msg.MessageId);
        }

        private void SendLocation(GeoLocationModel geoLocation, Message msg)
        {
           _botClient.SendLocationAsync(
                chatId: msg.Chat,
                latitude: geoLocation.Latitude,
                longitude: geoLocation.Longitude,
                livePeriod: 0,
                heading: 0,
                proximityAlertRadius: 0,
                disableNotification: false,
                replyToMessageId: 0,
                allowSendingWithoutReply: false,
                replyMarkup: null,
                cancellationToken: CancellationToken.None);
        }
    }
}
