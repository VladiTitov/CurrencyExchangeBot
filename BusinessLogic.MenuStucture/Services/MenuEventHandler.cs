using System.Linq;
using BusinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuEventHandler
    {
        public delegate void MessageAction();
        public event MessageAction Delete;

        private readonly ContainerPackerService _packerService;
        private readonly UserStateDTO _userState;

        public MenuEventHandler()
        {
            _packerService = new ContainerPackerService();
            _userState = _packerService.GetUserState(MenuEventService.UserId);
            _userState.Modify += SaveState;
        }

        public void MessageProcess(string message)
        {
            string text = message.Split("  ").LastOrDefault();
            switch (text)
            {
                case "/start":
                    _userState.StateId = 0;
                    break;
                case "Вернуться назад":
                    _userState.StepDown();
                    break;
                case "Купить":
                    _userState.UpdateBool(true);
                    break;
                case "Продать":
                    _userState.UpdateBool(false);
                    break;
                case "Выбрать город":
                    _userState.StepUp();
                    break;
                case string city when (_packerService.GetCities().Select(i=>i.NameRus).Distinct().Contains(city)):
                    _userState.UpdateCity(city);
                    break;
                case string currency when (_packerService.GetCurrencies().Select(i=>i.NameRus).Distinct().Contains($" {currency}")):
                    _userState.UpdateCurrency(currency);
                    break;
                case string bank when (_packerService.GetBanks().Select(i=>i.NameRus).Distinct().Contains(bank)):
                    _userState.UpdateBank(bank);
                    break;
            }
        }

        public void CallbackProcess(string message)
        {
            string text = message.Split("  ").LastOrDefault();
            switch (text)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "Close":
                    Delete?.Invoke();
                    _userState.StepDown();
                    break;
            }
        }

        private void SaveState() => _packerService.SaveUserState(_userState);
    }
}
