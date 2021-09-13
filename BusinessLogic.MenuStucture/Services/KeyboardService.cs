using System.Linq;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Services
{
    class KeyboardService
    {
        public string[][] GetRangeButtonsArray(string[] array, int range)
        {
            int count = array.Length / range + 1;

            return Enumerable.Range(0, count).
                Select(i => array.Skip(i * range)
                    .Take(range)
                    .ToArray()).
                ToArray();
        }

        public IEnumerable<KeyboardButton> GetKeyboardButtonModelButtons(string[] buttonLabels)
        {
            List<KeyboardButton> buttons = new List<KeyboardButton>();
            foreach (var btn in buttonLabels)
            {
                if (!btn.Equals("📍  Найти ближайшее")) buttons.Add(new KeyboardButton(btn));
                else buttons.Add(new KeyboardButton() { Text = btn, RequestLocation = true });
            }
            return buttons;
        }
    }
}
