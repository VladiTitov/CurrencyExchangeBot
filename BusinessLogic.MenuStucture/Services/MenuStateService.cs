using System;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Enums;
using BusinessLogic.MenuStucture.Keyboard;
using BusinessLogic.MenuStucture.Services.ModelsServices;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuStateService
    {
        private readonly ContainerPackerService _packerService;
        private readonly BankDTOService _bankService;
        private readonly BranchDTOService _branchService;
        private readonly CurrencyDTOService _currencyService;
        private readonly CityDTOService _cityService;
        private readonly UserStateDTO _userState;
        private IReplyMarkup _markup;

        private readonly string[] _buyOrSaleLabels = new[] { "Купить", "Продать"};
        private readonly string[] _selectCityOrLocationLabels = new[] { $"{MenuEmojiConstants.City}  Выбрать город", $"{MenuEmojiConstants.Location}  Найти ближайшее" };

        public MenuStateService()
        {
            _packerService = new ContainerPackerService();
            _bankService = new BankDTOService();
            _branchService = new BranchDTOService();
            _currencyService = new CurrencyDTOService();
            _cityService = new CityDTOService();
            _userState = _packerService.GetUserState(MenuEvent.UserId);
        }

        public (string message, IReplyMarkup markup) GetValues()
        {
            var state = (EnumStates.MenuStates)Enum.Parse(typeof(EnumStates.MenuStates), _userState.StateId.ToString());
            string message = MenuState(state);
            return (message, _markup);
        }

        private string MenuState(EnumStates.MenuStates state)
        {
            switch (state)
            {
                case EnumStates.MenuStates.SelectCityOrSendLocation:
                    _markup = new KeyboardButtonModel(_selectCityOrLocationLabels).GetButtonsKeyboard(false, false);
                    return $"Привет! {MenuEvent.UserName}\nДавай найдем лучший курс для обмена валют\U0001f609";

                case EnumStates.MenuStates.ShowCities:
                    string[] cities = _cityService.GetCitiesList();
                    _markup = new KeyboardButtonModel(cities).GetButtonsKeyboard(true, false, 3);
                    return $"{MenuEmojiConstants.City} Выбирай интересующий город и поехали дальше!";

                case EnumStates.MenuStates.ShowCurrencies:
                    string[] currencies = _currencyService.GetCurrencies(_userState.CityId);
                    _markup = new KeyboardButtonModel(currencies).GetButtonsKeyboard(true, false, 2);
                    return $"А теперь давай выберем валюту:";

                case EnumStates.MenuStates.BuyOrSell:
                    _markup = new KeyboardButtonModel(_buyOrSaleLabels).GetButtonsKeyboard(true, false);
                    return $"Будем покупать или продавать?:";

                //case MenuStates.ShowBestOffer:
                //    break;

                case EnumStates.MenuStates.ShowBanks:
                    string[] banks = _bankService.GetBanksNamesByCurrency(_userState.CurrencyId, _userState.CityId);
                    _markup = new KeyboardButtonModel(banks).GetButtonsKeyboard(true, true, 2);
                    return "А теперь давай выберем банк в который пойдем";

                case EnumStates.MenuStates.ShowBank:
                    string[] branchesTest = new[] {
                        "\U0001f4cd",
                        "\U0001f695",
                        "\u274C Закрыть"
                    };

                    var branches = _branchService.GetBranchesList(_userState.CurrencyId, _userState.CityId);
                    _markup = new InlineKeyboardButtonModel(branchesTest).GetInlineButtonsKeyboard(2);
                    return $"[\U0001f4cd] Банк\n В какое отделение банка пойдем?";

                case EnumStates.MenuStates.Location:
                    return "Location";

                default:
                    return "default";
            }
        }
    }
}
