using System;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Enums;
using BusinessLogic.MenuStucture.Keyboard;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Services.ModelsDtoServices;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuStateService
    {
        private readonly BankDTOService _bankService;
        private readonly BranchDTOService _branchService;
        private readonly CurrencyDTOService _currencyService;
        private readonly CityDTOService _cityService;
        private readonly QuotationDTOService _quotationService;
        private readonly UserStateDTO _userState;
        private readonly Message _message;

        private readonly string[] _buyOrSaleLabels = { $"{MenuEmojiConstants.Buy}  Купить", $"{MenuEmojiConstants.Sell}  Продать" };
        private readonly string[] _selectCityOrLocationLabels = { $"{MenuEmojiConstants.City}  Выбрать город", $"{MenuEmojiConstants.Location}  Найти ближайшее" };
        private readonly string[] _responseButtons = { $"{MenuEmojiConstants.Location}  Маршрут", $"{MenuEmojiConstants.Taxi}  Яндекс.Такси" };

        public MenuStateService(Message message)
        {
            _message = message;
            _bankService = new BankDTOService();
            _branchService = new BranchDTOService();
            _currencyService = new CurrencyDTOService();
            _quotationService = new QuotationDTOService();
            _cityService = new CityDTOService();
            _userState = new ContainerPackerService().GetUserState(message.Chat.Id);
        }

        public UserResponseModel GetMenuState()
        {
            var state = (EnumStates.MenuStates)Enum.Parse(typeof(EnumStates.MenuStates), _userState.StateId.ToString());
            switch (state)
            {
                #region Первый запуск, выбираем город или поиск по местоположению

                case EnumStates.MenuStates.SelectCityOrSendLocation:
                    string userName = $"{_message.Chat.FirstName} {_message.Chat.LastName}";
                    return new UserResponseModel(
                            $"Привет, {userName}!\nДавай найдем лучший курс для обмена валют\U0001f609", 
                            new KeyboardButtonModel(_selectCityOrLocationLabels).GetButtonsKeyboard(false, false)
                        );

                #endregion

                #region Показываем все города, для выбора
                case EnumStates.MenuStates.ShowCities:
                    return new UserResponseModel(
                            $"{MenuEmojiConstants.City} Выбирай интересующий город и поехали дальше!",
                            new KeyboardButtonModel(_cityService.GetCitiesList()).GetButtonsKeyboard(true, false, 2)
                        );
                #endregion

                #region Показываем все валюты в выбранном городе для выбора

                case EnumStates.MenuStates.ShowCurrencies:
                    return new UserResponseModel(
                            $"А теперь давай выберем валюту:",
                            new KeyboardButtonModel(_currencyService.GetCurrencies(_userState.CityId)).GetButtonsKeyboard(true, false, 2)
                        );

                #endregion

                #region Варианты действия купить/продать

                case EnumStates.MenuStates.BuyOrSell:
                    return  new UserResponseModel(
                            $"Будем покупать или продавать?:",
                            new KeyboardButtonModel(_buyOrSaleLabels).GetButtonsKeyboard(true, false, 2)
                        );

                #endregion

                #region Банки, которые могут предложить покупку/продажу выбранной валюты в выбранном городе

                case EnumStates.MenuStates.ShowBanks:
                    return new UserResponseModel(
                            "А теперь давай выберем банк в который пойдем",
                            new KeyboardButtonModel(_bankService.GetBanksNamesByCurrency(_userState.CurrencyId, _userState.CityId)).GetButtonsKeyboard(true, true, 2)
                        );

                #endregion

                #region Показываем все отделения банка и их предложения

                case EnumStates.MenuStates.ShowBranches:
                    string[] branchesName = _branchService.GetBranchesAndOffers(_userState.CurrencyId, _userState.CityId, _userState.BankId, _userState.Buy);
                    string bank = _bankService.GetBankNameById(_userState.BankId);
                    return 
                        new UserResponseModel(
                            $"[\U0001f4cd] {bank}[.]({BanksImagesLinks.ImagesLinks[bank]})\n В какое отделение банка пойдем?",
                            new InlineKeyboardButtonModel(branchesName, _userState.StateId).GetInlineButtonsKeyboard()
                        );

                #endregion

                case EnumStates.MenuStates.ShowBank:
                    return new UserResponseModel("Смотрим на банк!", null);

                case EnumStates.MenuStates.Location:
                    return new UserResponseModel("Ближайшие к нам отделения!", null);

                case EnumStates.MenuStates.ShowBestOffer:
                    var bestOffers = _quotationService.GetBestOffer(_userState.Buy, _userState.CityId, _userState.CurrencyId);
                    return new UserResponseModel("Лучшие предложения!", new InlineKeyboardButtonModel(bestOffers, _userState.StateId).GetInlineButtonsKeyboard());

                default:
                    return new UserResponseModel(
                            "DEFAULT",
                            null
                            );
            }
        }
    }
}
