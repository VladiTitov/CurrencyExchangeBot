using System.Linq;
using BusinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuEventHandler
    {
        private readonly ContainerPackerService _packerService;
        private readonly UserStateDTO _userState;
        private readonly string _message;

        public MenuEventHandler(string text)
        {
            _message = text;
            _packerService = new ContainerPackerService();
            _userState = _packerService.GetUserState(MenuEvent.UserId);
            _userState.Modify += SaveState;
        }

        public void Process()
        {
            string text = _message.Split("  ").LastOrDefault();
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
                case string city when (_packerService.GetCitiesNames().Contains(city)):
                    _userState.UpdateCity(city);
                    break;
                case string currency when (_packerService.GetCurrenciesNames().Contains($" {currency}")):
                    _userState.UpdateCurrency(currency);
                    break;
                case string bank when (_packerService.GetBanksNames().Contains(bank)):
                    _userState.UpdateBank(bank);
                    break;
            }
        }

        private void SaveState() => _packerService.SaveUserState(_userState);
    }
}
