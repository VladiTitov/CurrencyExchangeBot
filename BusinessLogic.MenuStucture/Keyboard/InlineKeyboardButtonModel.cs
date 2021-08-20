using System.Collections.Generic;
using BusinessLogic.MenuStucture.Constants;
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
                    btns.Add(new InlineKeyboardButton() { Text = $"{MenuEmojiConstants.Location}  {btn}", CallbackData = $"{btn.Length}" });
                }
                buttons.Add(btns.ToArray());
            }
            buttons.Add(new[] { new InlineKeyboardButton() { Text = $"{MenuEmojiConstants.Close}  Закрыть", CallbackData = "Close" } });
            return (new InlineKeyboardMarkup(buttons));
        }

        

    }
}
