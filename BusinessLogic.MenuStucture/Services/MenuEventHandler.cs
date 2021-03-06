using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.GeoParser.Models;
using BusinessLogic.MenuStucture.Models;
using BusinessLogic.MenuStucture.Models.Interfaces;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuEventHandler : IEventHandler
    {
        public delegate void DeleteMessageDelegate(Message message);
        public event DeleteMessageDelegate DeleteMessage;

        public delegate void SendMessageDelegate(UserResponseModel responseModel, Message message);
        public event SendMessageDelegate SendMessage;

        public delegate void SendLocationDelegate(GeoLocationModel geoLocation, Message message);
        public event SendLocationDelegate SendLocation;

        private readonly ContainerPackerService _packerService;
        private readonly UserStateDTO _userState;

        private readonly Message _message;

        public MenuEventHandler(Message message)
        {
            _message = message;
            _packerService = new ContainerPackerService();
            _userState = _packerService.GetUserState(message.Chat.Id);
            _userState.Modify += SaveState;
        }

        public void MessageProcess(string message)
        {
            StateHandler(message);
            MenuStateService stateService = new MenuStateService(_message); 
            var userResponseModel = stateService.GetMenuState();
            SendMessage?.Invoke(userResponseModel, _message);
        }

        private void StateHandler(string message)
        {
            if (message != null) switch (message)
            {
                case "/start":
                    _userState.UpdateState(0);
                    break;
                case "Найти ближайшее":
                    _userState.UpdateState(7);
                    break;
                case "Выбрать город":
                    _userState.UpdateState(1);
                    break;
                case string city when (_packerService.GetCities().Result.Select(i => i.NameRus).Distinct().Contains(city)):
                    _userState.UpdateCity(_packerService.GetCityId(city));
                    break;
                case string currency when (_packerService.GetCurrencies().Result.Select(i => i.NameRus).Distinct().Contains(currency)):
                    _userState.UpdateCurrency(_packerService.GetCurrencyId(currency));
                    break;
                case "Купить":
                    _userState.UpdateBool(true);
                    break;
                case "Продать":
                    _userState.UpdateBool(false);
                    break;
                case "Лучшее предложение в городе":
                    _userState.UpdateState(8);
                    break;
                case string bank when (_packerService.GetBanksAsync().Result.Select(i => i.NameRus).Distinct().Contains(bank)):
                    _userState.UpdateBank(_packerService.GetBankId(bank));
                    break;
                case "Вернуться назад":
                    _userState.StepDown();
                    break;
            }
        }

        public void CallbackProcess(string message)
        {
            if (message.Equals("Stage8-close"))
            {
                _userState.UpdateState(4);
                DeleteMessage?.Invoke(_message);
                CallbackEvent();
            }
            else if (message.Equals("Stage5-close"))
            {
                _userState.UpdateState(4);
                DeleteMessage?.Invoke(_message);
                CallbackEvent();
            }
            else if (message.Equals("Stage56-close"))
            {
                _userState.UpdateState(5);
                DeleteMessage?.Invoke(_message);
                CallbackEvent();
            }
            else if (message.Equals("Stage86-close"))
            {
                _userState.UpdateState(8);
                DeleteMessage?.Invoke(_message);
                CallbackEvent();
            }
            else if (int.TryParse(message, out var num))
            {
                _userState.UpdateBranch(num);
                DeleteMessage?.Invoke(_message);
                CallbackEvent();
            }
            else
            {
                var geoLocation = _packerService.GetLocation(_userState.BranchId);
                SendLocation?.Invoke(geoLocation, _message);
            }
        }

        private void CallbackEvent()
        {
            var stateService = new MenuStateService(_message);
            var userResponseModel = stateService.GetMenuState();
            SendMessage?.Invoke(userResponseModel, _message);
        }

        private void SaveState() => _packerService.SaveUserState(_userState);
    }
}
