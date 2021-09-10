using System;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Enums;
using BusinessLogic.MenuStucture.Keyboard;
using BusinessLogic.MenuStucture.Models;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Services
{
    public class MenuStateService
    {
        private readonly ContainerPackerService _packerService;
        private readonly UserStateDTO _userState;
        private readonly Message _message;

        private readonly string[] _buyOrSaleLabels = { $"{MenuEmojiConstants.Buy}  Купить", $"{MenuEmojiConstants.Sell}  Продать" };
        //private readonly string[] _selectCityOrLocationLabels = { $"{MenuEmojiConstants.City}  Выбрать город", $"{MenuEmojiConstants.Location}  Найти ближайшее" };
        private readonly string[] _selectCityOrLocationLabels = { $"{MenuEmojiConstants.City}  Выбрать город" };

        public MenuStateService(Message message)
        {
            _userState = new ContainerPackerService().GetUserState(message.Chat.Id);
            _message = message;
            _packerService = new ContainerPackerService();
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
                        $"Привет, {userName}, очень рад видеть тебя!{MenuEmojiConstants.WinkingFace}\nДавай я помогу тебе найти лучший курс для обмена валют в твоем городе!{MenuEmojiConstants.Hare}",
                        new KeyboardButtonModel(_selectCityOrLocationLabels).GetButtonsKeyboard(false, false)
                    );
                #endregion

                #region Показываем все города, для выбора
                case EnumStates.MenuStates.ShowCities:
                    return new UserResponseModel(
                            $"{MenuEmojiConstants.City} Вот список человеческих поселений, которые я знаю!\nВыбирай интересующий тебя вариант и поскакали дальше!{MenuEmojiConstants.Hare}",
                            new KeyboardButtonModel(_packerService.GetCitiesList()).GetButtonsKeyboard(true, false, 2)
                        );
                #endregion

                #region Показываем все валюты в выбранном городе для выбора
                case EnumStates.MenuStates.ShowCurrencies:
                    return new UserResponseModel(
                        $"Отличный выбор!{MenuEmojiConstants.ThumbsUp}\nА теперь давай выберем нужную бумажку, которую вы надываете деньгами {MenuEmojiConstants.DollarBanknotes}:",
                        new KeyboardButtonModel(_packerService.GetCurrenciesList(_userState.CityId)).GetButtonsKeyboard(true, false, 2)
                    );
                #endregion

                #region Варианты действия купить/продать

                case EnumStates.MenuStates.BuyOrSell:
                    return  new UserResponseModel(
                            $"Я тебя понял!{MenuEmojiConstants.WinkingFace}\nМне договариваться о покупке или продаже?{MenuEmojiConstants.Hare}",
                            new KeyboardButtonModel(_buyOrSaleLabels).GetButtonsKeyboard(true, false, 2)
                        );

                #endregion

                #region Банки, которые могут предложить покупку/продажу выбранной валюты в выбранном городе
                case EnumStates.MenuStates.ShowBanks:
                    return new UserResponseModel(
                            $"Вот банки твоего города, которые я помню!{MenuEmojiConstants.Bank}\nНо я всего лишь{MenuEmojiConstants.Hare}, мог что-то забыть.{MenuEmojiConstants.RelievedFace}",
                            new KeyboardButtonModel(_packerService.GetBanksNamesByCurrency(_userState.CurrencyId, _userState.CityId)).GetButtonsKeyboard(true, true, 2)
                        );
                #endregion

                #region Показываем все отделения банка и их предложения
                case EnumStates.MenuStates.ShowBranches:
                    var branchesName = _packerService.GetBranchesAndOffers(_userState.CurrencyId, _userState.BankId, _userState.CityId, _userState.Buy);
                    string bank = _packerService.GetBankNameById(_userState.BankId);
                    return
                        new UserResponseModel(
                            $"[\U0001f4cd] {bank}[.]({BanksImagesLinks.ImagesLinks[bank]})\nВ какое отделение банка пойдем в гости?",
                            new InlineKeyboardButtonModel(branchesName, _userState.StateId).GetInlineButtonsKeyboard()
                        );
                #endregion

                #region Смотрим банк
                case EnumStates.MenuStates.ShowBank:
                    var pr = int.Parse($"{_userState.PrevStateId}{_userState.StateId}");
                    var bankView = _packerService.GetBankFromBranch(_userState.BranchId, _userState.CurrencyId);
                    return new UserResponseModel(bankView.ToString(),
                        new InlineKeyboardButtonModel(pr).GetBankButtons(bankView.BranchAdr));
                #endregion

                case EnumStates.MenuStates.Location:
                    return new UserResponseModel("Ближайшие к нам отделения!", null);

                #region Показываем лучшее предложение
                case EnumStates.MenuStates.ShowBestOffer:
                    var bestOffers = _packerService.GetBestOffer(_userState.CityId, _userState.CurrencyId, _userState.Buy);
                    return new UserResponseModel($"Специально для тебя я нашел лучшие предложения!{MenuEmojiConstants.Hare}\nВыбирай скорее! Уже совсем скоро они могут быть неактуальными!{MenuEmojiConstants.RelievedFace}", new InlineKeyboardButtonModel(bestOffers, _userState.StateId).GetInlineButtonsKeyboard());
                #endregion

                default:
                    return new UserResponseModel(
                            $"Извини, я всего лишь {MenuEmojiConstants.Hare}.\nЯ знаю лишь пару слов по-человечески. Нажми на /start и начнем сначала!",
                            null
                            );
            }
        }
    }
}
