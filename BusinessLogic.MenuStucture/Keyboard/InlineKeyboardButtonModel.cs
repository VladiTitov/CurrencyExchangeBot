using System.Linq;
using System.Collections.Generic;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Keyboard.RequestModels;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Keyboard
{
    public class InlineKeyboardButtonModel
    {
        private readonly KeyboardService _keyboardService;
        private readonly List<BestOffersModel> _buttonsLabels;
        private readonly int _userStateId;

        public InlineKeyboardButtonModel(List<BestOffersModel> buttonsLabels, int userStateId)
        {
            _keyboardService = new KeyboardService();
            _buttonsLabels = buttonsLabels;
            _userStateId = userStateId;
        }

        public InlineKeyboardButtonModel(int userStateId)
        {
            _keyboardService = new KeyboardService();
            _userStateId = userStateId;
        }

        public IReplyMarkup GetInlineButtonsKeyboard(int columns = 1)
        {
            var newButtonsArray = GetRangeButtonsArray(_buttonsLabels, columns);
            IEnumerable<IEnumerable<InlineKeyboardButton>> buttons = GetButtonArray(newButtonsArray);
            return new InlineKeyboardMarkup(buttons);
        }

        private IEnumerable<IEnumerable<InlineKeyboardButton>> GetButtonArray(List<List<BestOffersModel>> buttonLabels)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new List<IEnumerable<InlineKeyboardButton>>();
            foreach (var buttonLabel in buttonLabels)
            {
                buttons.Add(GetButtons(buttonLabel));
            }
            buttons.Add(new[] 
            { 
                new InlineKeyboardButton()
                {
                    Text = $"{MenuEmojiConstants.Close}  Закрыть", 
                    CallbackData = $"Stage{_userStateId}-close"
                }
            });

            return buttons;
        }

        private IEnumerable<InlineKeyboardButton> GetButtons(List<BestOffersModel> buttonLabels)
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            foreach (var btn in buttonLabels)
            {
                buttons.Add(new InlineKeyboardButton() { Text = btn.ToString(), CallbackData = $"{btn.BankAdrId}" });
            }
            return buttons;
        }

        public IReplyMarkup GetBankButtons(string adr)
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton() { Text = $"{MenuEmojiConstants.Location}  Где это?", CallbackData = adr },
                new InlineKeyboardButton() { Text = $"{MenuEmojiConstants.Close}  Закрыть", CallbackData = $"Stage{_userStateId}-close" }
            });
        }

        private List<List<BestOffersModel>> GetRangeButtonsArray(List<BestOffersModel> list, int range)
        {
            int count = list.Count / range + 1;

            return Enumerable.Range(0, count).
                Select(i => list.Skip(i * range)
                    .Take(range)
                    .ToList()).
                ToList();
        }
    }
}
