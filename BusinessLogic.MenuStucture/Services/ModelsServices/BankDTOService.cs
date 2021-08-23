using BusinessLogic.MenuStucture.Constants;
using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{

    class BankDTOService
    {
        private readonly ContainerPackerService _packerService;
        public BankDTOService() => _packerService = new ContainerPackerService();
        public string[] GetBanksNamesByCurrency(int currencyId, int cityId)
        {
            var banks = new BranchDTOService().GetBranchesList(currencyId, cityId).Select(i => i.Bank.NameRus).Distinct().ToArray();
            for (int i = 0; i < banks.Length; i++)
            {
                banks[i] = $"{MenuEmojiConstants.Bank}  {banks[i]}";
            }

            return banks;
        }

        public string GetBankNameById(int id) => 
            _packerService.GetBanks().FirstOrDefault(i => i.Id.Equals(id)).NameRus;
    }
}
