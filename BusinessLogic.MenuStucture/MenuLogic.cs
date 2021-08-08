using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using BisinessLogic.Database;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Models;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture
{
    public class MenuLogic
    {

        private enum MenuStates
        {
            SelectCityOrSendLocation,
            ShowCities,
            ShowCurrencies,
            BuyOrSell,
            //ShowBestOffer,
            ShowBanks,
            Location,
            ShowBank
        }

        //private Dictionary<MenuStates, string[]> menuDictionary = new Dictionary<MenuStates, string[]>();

        private static ITelegramBotClient _botClient;
        private readonly ICityService _cityService;
        private readonly ICurrencyService _currencyService;
        private readonly IBankService _bankService;
        private readonly IUserStateService _stateService;
        private readonly IBranchService _branchService;
        private readonly IQuotationService _quotationService;
        private static IReplyMarkup _markup;

        private readonly string[] _cities;
        private string[] _currencies;
        private string[] _banks;
        private List<BranchDTO> _branches;

        private UserStateDTO UserState;

        private Chat _chat;

        private int State = 0;

        public MenuLogic(
            ICityService cityService,
            ICurrencyService currencyService,
            IBankService bankService,
            IBranchService branchService,
            IUserStateService userStateService,
            IQuotationService quotationService)
        {
            _cityService = cityService;
            _currencyService = currencyService;
            _bankService = bankService;
            _branchService = branchService;
            _stateService = userStateService;
            _quotationService = quotationService;

            _cities = _cityService.GetData().Select(i => i.NameRus).ToArray();
            _banks = _bankService.GetData().Select(i => i.NameRus).ToArray();
            _currencies = _currencyService.GetData().Select(i => i.NameRus).ToArray();

            //menuDictionary.Add(MenuStates.ShowCities, _cities);
            //menuDictionary.Add(MenuStates.ShowBanks, _banks);
            //menuDictionary.Add(MenuStates.ShowCurrencies, _currencies);
        }

        public async void Start(Chat chat, ITelegramBotClient botClient, string text)
        {
            _botClient = botClient;
            _chat = chat;
           
            if (!IsStateExist())
            {
                SaveState();
            }
            UserState = _stateService.GetState(_chat.Id);
            //State = _stateService.GetState(_chat.Id).State;

            State = UserState.State;
            string message = MenuStep(text);

            var state = (MenuStates) Enum.Parse(typeof(MenuStates), State.ToString());
            MenuState(state);

            try
            {
                await SendMessage(message, _markup);
            }
            catch
            {

            }
        }

        private string MenuStep(string text)
        {
            text = text.Split("  ").LastOrDefault();
            switch (text)
            {
                case "/start":
                    UserState.State = 0;
                    SaveState();
                    return $"Привет, {_chat.FirstName} {_chat.LastName}!\nДавай найдем лучший курс для обмена валют\U0001f609";
                case "Вернуться назад":
                    UserState.State--;
                    SaveState();
                    return null;
                case "Купить":
                    UserState.State++;
                    UserState.Buy = true;
                    SaveState();
                    return "А теперь давай выберем банк в который пойдем";
                case "Продать":
                    UserState.State++;
                    UserState.Buy = false;
                    SaveState();
                    return "А теперь давай выберем банк в который пойдем";
                case "Выбрать город":
                    UserState.State++; ;
                    SaveState();
                    return $"{MenuEmojiConstants.City} Выбирай интересующий город и поехали дальше!";
                case string city when (_cityService.GetData().Select(i => i.NameRus).Contains(city)):
                    UserState.State++;
                    UserState.City = city;
                    SaveState();
                    return $"А теперь давай выберем валюту:";
                case string currency when (_currencyService.GetData().Select(i => i.NameRus).Contains($" {currency}")):
                    UserState.State++;
                    UserState.Currency = currency;
                    SaveState();
                    return $"Будем покупать или продавать?:";
                default:
                    //_markup = new InlineKeyboardMarkup("text") { };
                    return null;
            }
        }

        private void MenuState(MenuStates state)
        {
            switch (state)
            {
                case MenuStates.SelectCityOrSendLocation:
                    _markup = new KeyboardModel(new[]
                        {
                            $"{MenuEmojiConstants.City}  Выбрать город",
                            $"{MenuEmojiConstants.Location}  Найти ближайшее"
                        })
                        .GetButtonsKeyboard(false, false);
                    break;
                case MenuStates.ShowCities:
                    _markup = new KeyboardModel(_cities).GetButtonsKeyboard(true,false, 3);
                    break;
                case MenuStates.ShowCurrencies:
                    var id = _cityService.GetData().FirstOrDefault(i => i.NameRus.Equals(UserState.City)).Id;
                    _branches = _branchService.GetBranchInCity(id).ToList();
                    var branchID = _branches.Select(i => i.Id).Distinct();
                    var quotations = _quotationService.GetData().Where(i => branchID.Contains(i.BranchDtoId)).ToList();
                    var currencies = _currencyService.GetData().Where(i => quotations.Select(i => i.CurrencyDtoId).ToList().Contains(i.Id)).ToArray().Select(i => i.NameRus).Distinct().ToArray();
                    _markup = new KeyboardModel(_currencies).GetButtonsKeyboard(true, false,2);
                    break;
                case MenuStates.BuyOrSell:
                    _markup = new KeyboardModel(new[]
                        {
                            "Купить",
                            "Продать"
                        })
                        .GetButtonsKeyboard(true, false);
                    break;
                //case MenuStates.ShowBestOffer:
                //    break;
                case MenuStates.ShowBanks:
                    var banks = _branchService.GetBranchInCity(_cityService.GetData().FirstOrDefault(i => i.NameRus.Equals(UserState.City)).Id).Select(i=>i.Bank).ToList();
                    var names = banks.Select(i => i.NameRus).Distinct().ToArray();
                    _markup = new KeyboardModel(names).GetButtonsKeyboard(true,true, 2);
                    break;
                case MenuStates.ShowBank:
                    break;
                case MenuStates.Location:
                    break;
            }
        }

        private Task SendMessage(string text, IReplyMarkup replyMarkup) =>
            _botClient.SendTextMessageAsync(
                chatId: _chat,
                text: text, ParseMode.Default,
                null,
                true,
                false,
                0,
                false,
                replyMarkup,
                CancellationToken.None);

        private void SaveState()
        {
            long userId = _chat.Id;
            var userState = _stateService.GetState(userId);
            if (userState != null)
            {
                userState = UserState;
                _stateService.Update(userState);
            }
            else _stateService.Add(new UserStateDTO() { UserId = userId, State = 0 }) ;
        }

        private bool IsStateExist() => _stateService.GetState(_chat.Id) != null;
    }
}
