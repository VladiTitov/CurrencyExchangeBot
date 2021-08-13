using System;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Enums;
using BusinessLogic.MenuStucture.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuStateService
    {
        private readonly ContainerPackerService _packerService;
        private readonly UserStateDTO _userState;
        private IReplyMarkup _markup;

        public MenuStateService()
        {
            _packerService = new ContainerPackerService();
            _userState = _packerService.GetUserState(MenuEvent.UserId);
        }

        public (string message, IReplyMarkup markup) GetValues()
        {
            var state = (EnumStates.MenuStates)Enum.Parse(typeof(EnumStates.MenuStates), _userState.State.ToString());
            string message = MenuState(state);
            return (message, _markup);
        }

        private string MenuState(EnumStates.MenuStates state)
        {
            switch (state)
            {
                case EnumStates.MenuStates.SelectCityOrSendLocation:
                    _markup = new KeyboardModel(new[]
                        {
                            $"{MenuEmojiConstants.City}  Выбрать город",
                            $"{MenuEmojiConstants.Location}  Найти ближайшее"
                        })
                        .GetButtonsKeyboard(false, false);
                    return $"Привет! {MenuEvent.UserName}\nДавай найдем лучший курс для обмена валют\U0001f609";

                case EnumStates.MenuStates.ShowCities:
                    string[] cities = _packerService.GetCitiesNames();
                    _markup = new KeyboardModel(cities).GetButtonsKeyboard(true, false, 3);
                    return $"{MenuEmojiConstants.City} Выбирай интересующий город и поехали дальше!";

                case EnumStates.MenuStates.ShowCurrencies:
                    //need city
                    string[] currencies = _packerService.GetCurrencies(_userState.City);
                    _markup = new KeyboardModel(currencies).GetButtonsKeyboard(true, false, 2);
                    return $"А теперь давай выберем валюту:";

                case EnumStates.MenuStates.BuyOrSell:
                    _markup = new KeyboardModel(new[]
                        {
                            "Купить",
                            "Продать"
                        })
                        .GetButtonsKeyboard(true, false);
                    return $"Будем покупать или продавать?:";

                //case MenuStates.ShowBestOffer:
                //    break;
                case EnumStates.MenuStates.ShowBanks:
                    string[] banks = _packerService.GetBanksNames(_userState.City);
                    _markup = new KeyboardModel(banks).GetButtonsKeyboard(true, true, 2);
                    return "А теперь давай выберем банк в который пойдем";

                case EnumStates.MenuStates.ShowBank:
                    return "А теперь давай выберем банк в который пойдем";

                case EnumStates.MenuStates.Location:
                    return "Location";

                default:
                    return "default";
            }
        }
    }
}
