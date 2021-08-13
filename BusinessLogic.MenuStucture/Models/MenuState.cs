using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Models
{
    public class MenuState
    {
        public IReplyMarkup Markup { get; }
        public string Message { get; }

        public MenuState()
        {
            var values = new MenuStateService().GetValues();
            Markup = values.markup;
            Message = values.message;
        }
    }
}
