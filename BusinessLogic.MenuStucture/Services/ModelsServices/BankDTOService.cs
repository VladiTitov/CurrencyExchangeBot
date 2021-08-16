using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class BankDTOService
    {
        public string[] GetBanksNamesByCurrency(string currencyName, string cityName) => 
            new BranchDTOService().GetBranchesList(currencyName, cityName).Select(i => i.Bank.NameRus).Distinct().ToArray();
    }
}
