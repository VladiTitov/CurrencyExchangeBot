using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Models.Interfaces;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuEventHandler : IEventHandler
    {
        public delegate void DeleteMessageDelegate(Message message);
        public event DeleteMessageDelegate Delete;

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
            //var pr = _packerService.GetBanks();

            switch (message)
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
                case string city when (_packerService.GetCities().Select(i => i.NameRus).Distinct().Contains(city)):
                    _userState.UpdateCity(_packerService.GetCityId(city));
                    break;
                case string currency when (_packerService.GetCurrencies().Select(i => i.NameRus).Distinct().Contains(currency)):
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
                case string bank when (_packerService.GetBanks().Select(i => i.NameRus).Distinct().Contains(bank)):
                    _userState.UpdateBank(_packerService.GetBankId(bank));
                    break;
                case "Вернуться назад":
                    _userState.StepDown();
                    break;
            }
        }

        public void CallbackProcess(string message)
        {
            switch (message)
            {
                case "Stage8-close":
                    _userState.UpdateState(4);
                    Delete?.Invoke(_message);
                    break;
                case "Stage5-close":
                    _userState.UpdateState(4);
                    Delete?.Invoke(_message);
                    break;
                default:
                    _userState.UpdateState(6);
                    Delete?.Invoke(_message);
                    break;
            }
        }

        private void SaveState() => _packerService.SaveUserState(_userState);
    }
}
