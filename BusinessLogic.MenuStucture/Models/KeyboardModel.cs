using System.Collections.Generic;
using System.Linq;
using BusinessLogic.MenuStucture.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Models
{
     public class KeyboardModel
    {
        private readonly string[] _buttonsArray;
        public KeyboardModel(string[] buttonsArray) => _buttonsArray = buttonsArray;

        public IReplyMarkup GetButtonsKeyboard(bool backButton, bool isBanks, int columns = 1)
        {
            string[][] newButtonsArray = GetRangeArray(_buttonsArray, columns);
            List<KeyboardButton[]> buttons = new List<KeyboardButton[]>();
            if (isBanks) buttons.Add(new[] { new KeyboardButton($"{MenuEmojiConstants.BestOffer}  Лучшее предложение в городе") });
            for (int i = 0; i < newButtonsArray.Length; i++)
            {
                List<KeyboardButton> btns = new List<KeyboardButton>();
                foreach (var btn in newButtonsArray[i])
                {
                    btns.Add(new KeyboardButton(btn));
                }
                buttons.Add(btns.ToArray());
            }

            if (backButton) buttons.Add(new[] {new KeyboardButton($"{MenuEmojiConstants.BackButton}  Вернуться назад")});

            return (new ReplyKeyboardMarkup
            {
                Keyboard = buttons,
                ResizeKeyboard = true,
                //OneTimeKeyboard = true
            });
        }

        public IReplyMarkup GetInlineButtonsKeyboard(int columns = 1)
        {
            var newButtonsArray = GetRangeArray(_buttonsArray, 3);
            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < newButtonsArray.Length; i++)
            {
                List<InlineKeyboardButton> btns = new List<InlineKeyboardButton>();
                foreach (var btn in newButtonsArray[i])
                {
                    btns.Add(new InlineKeyboardButton() {Text = btn, CallbackData = btn });
                }
                buttons.Add(btns.ToArray());
            }
            return (new InlineKeyboardMarkup(buttons));
        }

        private string[][] GetRangeArray(string[] array, int range)
        {
            int count = array.Length / range + 1;

            return Enumerable.Range(0, count).
                Select(i => array.Skip(i * range)
                    .Take(range)
                    .ToArray()).
                ToArray();
        }
    }
}
