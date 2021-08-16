using System.Collections.Generic;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Keyboard
{
    public class InlineKeyboardButtonModel
    {
        private readonly KeyboardService _keyboardService;
        private readonly string[] _buttonsLabels;

        public InlineKeyboardButtonModel(string[] buttonsLabels)
        {
            _keyboardService = new KeyboardService();
            _buttonsLabels = buttonsLabels;
        }

        public IReplyMarkup GetInlineButtonsKeyboard(int columns = 1)
        {
            var newButtonsArray = _keyboardService.GetRangeButtonsArray(_buttonsLabels, columns);
            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < newButtonsArray.Length; i++)
            {
                List<InlineKeyboardButton> btns = new List<InlineKeyboardButton>();
                foreach (var btn in newButtonsArray[i])
                {
                    btns.Add(new InlineKeyboardButton() { Text = btn, CallbackData = btn });
                }
                buttons.Add(btns.ToArray());
            }
            return (new InlineKeyboardMarkup(buttons));
        }
    }
}
