using System.Collections.Generic;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Keyboard
{
    public class KeyboardButtonModel
    {
        private readonly KeyboardService _keyboardService;
        private readonly string[] _buttonsLabels;

        public KeyboardButtonModel(string[] buttonsLabels)
        {
            _keyboardService = new KeyboardService();
            _buttonsLabels = buttonsLabels;
        }

        public IReplyMarkup GetButtonsKeyboard(bool backButton, bool isBanks, int columns = 1)
        {
            string[][] newButtonsArray = _keyboardService.GetRangeButtonsArray(_buttonsLabels, columns);
            IEnumerable<IEnumerable<KeyboardButton>> buttons = GetButtonArray(newButtonsArray, isBanks, backButton);
            return (new ReplyKeyboardMarkup
            {
                Keyboard = buttons,
                ResizeKeyboard = true,
                OneTimeKeyboard = isBanks
            });
        }

        public IEnumerable<IEnumerable<KeyboardButton>> GetButtonArray(string[][] buttonLabels, bool isBanks, bool backButton)
        {
            List<IEnumerable<KeyboardButton>> buttons = new List<IEnumerable<KeyboardButton>>();
            if (isBanks) buttons.Add(new[] { new KeyboardButton($"{MenuEmojiConstants.Shock}  Лучшее предложение в городе") });
            for (int i = 0; i < buttonLabels.Length; i++)
            {
                buttons.Add(_keyboardService.GetKeyboardButtonModelButtons(buttonLabels[i]));
            }
            if (backButton) buttons.Add(new[] { new KeyboardButton($"{MenuEmojiConstants.BackButton}  Вернуться назад") });

            return buttons;
        }
    }
}
