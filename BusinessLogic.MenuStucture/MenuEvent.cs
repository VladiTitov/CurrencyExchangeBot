using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture
{
    public class MenuEvent
    {
        private readonly ITelegramBotClient _botClient;
        private readonly Chat _chat;

        public static long UserId;
        public static string UserName;

        public MenuEvent(Chat chat, ITelegramBotClient botClient)
        {
            _botClient = botClient;
            _chat = chat;
            UserName = $"{_chat.FirstName} {_chat.LastName}";
            UserId = _chat.Id;
        }

        public async void Start(string text)
        {
            MenuEventHandler handler = new MenuEventHandler(text);
            handler.Process();

            MenuState state = new MenuState();
            await SendMessage(state);
        }

        private Task SendMessage(MenuState state)
        {
            string text = state.Message;
            IReplyMarkup replyMarkup = state.Markup;
            return _botClient.SendTextMessageAsync(
                chatId: _chat,
                text: text, ParseMode.Default,
                null,
                true,
                false,
                0,
                false,
                replyMarkup,
                CancellationToken.None);
        }
    }
}
