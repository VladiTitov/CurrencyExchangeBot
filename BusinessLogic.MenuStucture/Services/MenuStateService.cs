using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Enums;
using BusinessLogic.MenuStucture.Keyboard;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Services.ModelsServices;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuStateService
    {
        private readonly ContainerPackerService _packerService;
        private readonly BankDTOService _bankService;
        private readonly BranchDTOService _branchService;
        private readonly CurrencyDTOService _currencyService;
        private readonly CityDTOService _cityService;
        private readonly QuotationDTOService _quotationService;
        private readonly UserStateDTO _userState;

        private readonly string[] _buyOrSaleLabels = { $"{MenuEmojiConstants.Buy}  Купить", $"{MenuEmojiConstants.Sell}  Продать" };
        private readonly string[] _selectCityOrLocationLabels = { $"{MenuEmojiConstants.City}  Выбрать город", $"{MenuEmojiConstants.Location}  Найти ближайшее" };
        private readonly string[] _responseButtons = { $"{MenuEmojiConstants.Location}  Маршрут", $"{MenuEmojiConstants.Taxi}  Яндекс.Такси" };

        public MenuStateService()
        {
            _packerService = new ContainerPackerService();
            _bankService = new BankDTOService();
            _branchService = new BranchDTOService();
            _currencyService = new CurrencyDTOService();
            _quotationService = new QuotationDTOService();
            _cityService = new CityDTOService();
            _userState = _packerService.GetUserState(MenuEventService.UserId);
        }

        public List<UserResponseModel> GetMenuState()
        {
            var state = (EnumStates.MenuStates)Enum.Parse(typeof(EnumStates.MenuStates), _userState.StateId.ToString());
            switch (state)
            {
                case EnumStates.MenuStates.SelectCityOrSendLocation:
                    return new List<UserResponseModel>() 
                    {
                        new UserResponseModel(
                            $"Привет! {MenuEventService.UserName}\nДавай найдем лучший курс для обмена валют\U0001f609", 
                            new KeyboardButtonModel(_selectCityOrLocationLabels).GetButtonsKeyboard(false, false)
                            ) 
                    };

                case EnumStates.MenuStates.ShowCities:
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            $"{MenuEmojiConstants.City} Выбирай интересующий город и поехали дальше!",
                            new KeyboardButtonModel(_cityService.GetCitiesList()).GetButtonsKeyboard(true, false, 2)
                            )
                    };
                case EnumStates.MenuStates.ShowCurrencies:
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            $"А теперь давай выберем валюту:",
                            new KeyboardButtonModel(_currencyService.GetCurrencies(_userState.CityId)).GetButtonsKeyboard(true, false, 2)
                            )
                    };
                case EnumStates.MenuStates.BuyOrSell:
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            $"Будем покупать или продавать?:",
                            new KeyboardButtonModel(_buyOrSaleLabels).GetButtonsKeyboard(true, false, 2)
                            )
                    };
                case EnumStates.MenuStates.ShowBanks:
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            "А теперь давай выберем банк в который пойдем",
                            new KeyboardButtonModel(_bankService.GetBanksNamesByCurrency(_userState.CurrencyId, _userState.CityId)).GetButtonsKeyboard(true, true, 2)
                            )
                    };
                case EnumStates.MenuStates.ShowBestOffer:
                    var bestOffers = _quotationService.GetBestOffer(_userState.Buy, _userState.CityId);
                    return GetResponseList(bestOffers);

                case EnumStates.MenuStates.ShowBank:
                    string[] branches = _branchService.GetBranchesList(_userState.CurrencyId, _userState.CityId).Select(i => i.Adr).ToArray();
                    string bankName = _bankService.GetBankNameById(_userState.CurrencyId);
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            $"[\U0001f4cd] {bankName}\n В какое отделение банка пойдем?",
                            new InlineKeyboardButtonModel(branches).GetInlineButtonsKeyboard()
                            )
                    };
                //case EnumStates.MenuStates.Location:
                //    return "Location";

                default:
                    return new List<UserResponseModel>()
                    {
                        new UserResponseModel(
                            "DEFAULT",
                            null
                            )
                    };
            }
        }

        public List<UserResponseModel> GetResponseList(string[] data)
        {
            List<UserResponseModel> response = new List<UserResponseModel>();
            foreach(var d in data)
            {
                response.Add(new UserResponseModel(d, new InlineKeyboardButtonModel(_responseButtons).GetInlineButtonsKeyboard(2)));
            }
            return response;
        }
    }
}
