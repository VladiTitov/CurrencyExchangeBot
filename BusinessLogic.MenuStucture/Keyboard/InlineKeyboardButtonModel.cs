using System.Collections.Generic;
using System.Text.Json.Serialization;
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
            IEnumerable<IEnumerable<InlineKeyboardButton>> buttons = GetButtonArray(newButtonsArray);
            return new InlineKeyboardMarkup(buttons);
        }

        public IEnumerable<IEnumerable<InlineKeyboardButton>> GetButtonArray(string[][] buttonLabels)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new List<IEnumerable<InlineKeyboardButton>>();
            for (int i = 0; i < buttonLabels.Length; i++)
            {
                buttons.Add(GetButtons(buttonLabels[i]));
            }
            buttons.Add(new[] { new InlineKeyboardButton() { Text = $"{MenuEmojiConstants.Close}  Закрыть", CallbackData = "Close" } });

            return buttons;
        }

        public IEnumerable<InlineKeyboardButton> GetButtons(string[] buttonLabels)
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (var btn in buttonLabels)
            {
                buttons.Add(new InlineKeyboardButton() { Text = btn, CallbackData = "1" });
            }
            return buttons;
        }
    }
}
