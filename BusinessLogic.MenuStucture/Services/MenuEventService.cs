using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Models.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuEventService
    {
        private IHandler _handler;
        private readonly ITelegramBotClient _botClient;

        public static long UserId;
        public static string UserName;

        public MenuEventService(ITelegramBotClient botClient) => 
            _botClient = botClient;

        public async void Process(IHandler handler)
        {
            _handler = handler;
            UserName = $"{_handler.Message.Chat.FirstName} {_handler.Message.Chat.LastName}";
            UserId = _handler.Message.Chat.Id;
            _handler.Process();

            MenuState menuState = new MenuState();
            await SendMessage(menuState);
        }

        private Task SendMessage(MenuState state)
        {
            string text = state.Message;
            IReplyMarkup replyMarkup = state.Markup;
            return _botClient.SendTextMessageAsync(
                chatId: _handler.Message.Chat,
                text: text, ParseMode.Default,
                null,
                true,
                false,
                0,
                false,
                replyMarkup,
                CancellationToken.None);
        }

        private void DeleteMessage()
        {
            _botClient.DeleteMessageAsync(
                chatId: _handler.Message.Chat,
                messageId: _handler.Message.MessageId);
        }
    }
}
