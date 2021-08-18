using System.Collections.Generic;
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
            List<KeyboardButton[]> buttons = new List<KeyboardButton[]>();
            if (isBanks) buttons.Add(new[] { new KeyboardButton($"Лучшее предложение в городе") });
            for (int i = 0; i < newButtonsArray.Length; i++)
            {
                List<KeyboardButton> btns = new List<KeyboardButton>();
                foreach (var btn in newButtonsArray[i])
                {
                    btns.Add(new KeyboardButton(btn));
                }
                buttons.Add(btns.ToArray());
            }

            if (backButton) buttons.Add(new[] { new KeyboardButton($"Вернуться назад") });

            return (new ReplyKeyboardMarkup
            {
                Keyboard = buttons,
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            });
        }
    }
}
