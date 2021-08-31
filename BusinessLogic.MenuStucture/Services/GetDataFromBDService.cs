using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Keyboard.RequestModels;

namespace BusinessLogic.MenuStucture.Services
{
    class GetDataFromBDService
    {
        private class BestOffers
        {
            public int BranchId { get; }
            public string Offer { get; }
            public BestOffers(int branchId, string offer) => 
                (BranchId, Offer) = (branchId, offer);
        }

        private readonly ICityService _cityService;
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly IQuotationService _quotationService;
        private readonly ICurrencyService _currencyService;
        private readonly IUserStateService _userStateService;

        public GetDataFromBDService(ICityService cityService,
            IBankService bankService,
            IBranchService branchService,
            IQuotationService quotationService,
            ICurrencyService currencyService,
            IUserStateService userStateService)
        {
            _cityService = cityService;
            _bankService = bankService;
            _branchService = branchService;
            _quotationService = quotationService;
            _currencyService = currencyService;
            _userStateService = userStateService;
        }

        #region Base CRUD
        public IEnumerable<CityDTO> GetCities() => _cityService.GetData();
        public IEnumerable<BankDTO> GetBanks() => _bankService.GetData();
        public IEnumerable<CurrencyDTO> GetCurrencies() => _currencyService.GetData();

        public int GetCitiesId(string name) =>
            _cityService.GetData()
                .FirstOrDefault(i => i.NameRus.Equals(name)).Id;
        public int GetBanksId(string name) =>
            _bankService.GetData()
                .FirstOrDefault(i => i.NameRus.Equals(name)).Id;
        public int GetBranchesId(string name) =>
            _branchService.GetData()
                .FirstOrDefault(i => i.Adr.Equals(name)).Id;
        public int GetCurrenciesId(string name) =>
            _currencyService.GetData()
                .FirstOrDefault(i => i.NameRus.Equals(name)).Id;


        #endregion

        #region CitiesRegion
        public string[] GetCitiesList()
        {
            var cities = _cityService.GetData().Select(i => i.NameRus).ToArray();
            for (int i = 0; i < cities.Length; i++)
            {
                cities[i] = $"{MenuEmojiConstants.City}  {cities[i]}";
            }
            return cities;
        }
        #endregion

        #region BanksRegion

        public string[] GetBanksNamesByCurrency(int currencyId, int cityId)
        {
            var banks = GetBranchesList(currencyId, cityId).Select(i => i.Bank.NameRus).Distinct().ToArray();
            for (int i = 0; i < banks.Length; i++)
            {
                banks[i] = $"{MenuEmojiConstants.Bank}  {banks[i]}";
            }
            return banks;
        }

        public string GetBankNameById(int id) => _bankService.GetData().FirstOrDefault(i => i.Id.Equals(id))?.NameRus;
        #endregion

        #region BranchesRegion
        private List<BranchDTO> GetBranchesList(int currencyId, int cityId)
        {
            var branchList = _branchService.GetData().Where(i => i.CityId.Equals(cityId)).ToList();
            var quotationList = _quotationService.GetData().Where(i => i.CurrencyId.Equals(currencyId)).Select(i => i.BranchId).ToList();

            List<BranchDTO> result = new List<BranchDTO>();

            foreach (var branch in branchList)
            {
                if (quotationList.Contains(branch.Id)) result.Add(branch);
            }

            return result;
        }

        public void GetBank(int bankId, int branchId)
        {

        }

        public List<BestOffersModel> GetBranchesAndOffers(int currencyId, int bankId, int cityId, bool key)
        {
            var branches = GetBranchesList(currencyId, cityId).Where(i => i.BankId.Equals(bankId)).ToList();
            return GetQuotationByBranches(currencyId, branches, key);
        }

        public List<BranchDTO> GetBranchesList(IEnumerable<int> idList) =>
            _branchService.GetData().Where(i => idList.Contains(i.Id)).Distinct().ToList();


        private IEnumerable<BranchDTO> GetBranchesInCity(int id) =>
            _branchService.GetData().Where(i => i.CityId.Equals(id));

        #endregion

        #region QuotationsRegion
        public IEnumerable<QuotationDTO> GetQuotations(int cityId)
        {
            var branches = _branchService.GetData().Where(i => i.CityId.Equals(cityId)).ToList();
            return _quotationService.GetData().Where(i => branches.Select(j => j.Id).ToList().Contains(i.BranchId)).Distinct().ToList();
        }

        public List<BestOffersModel> GetBestOffer(int cityId, int currencyId, bool key)
        {
            List<QuotationDTO> quotations = _quotationService.GetData().Where(i => i.CurrencyId.Equals(currencyId)).ToList();
            var bestOffers = key ? BestOfferSale(quotations) : BestOfferBuy(quotations);
            List<int> branchesId = bestOffers.Select(i => i.BranchId).ToList();
            var branches = GetBranchesList(branchesId);
            List<BestOffersModel> result = new List<BestOffersModel>();

            for (int i = 0; i < branches.Count(); i++)
            {
                result.Add(new BestOffersModel(branches[i].BankId, branches[i].Bank.NameRus, branches[i].Id, branches[i].Adr, bestOffers[i].Offer));
            }

            return result;
        }

        public List<BestOffersModel> GetQuotationByBranches(int currencyId, List<BranchDTO> branches, bool key)
        {
            List<BestOffersModel> result = new List<BestOffersModel>();

            var quotations = _quotationService.GetData()
                .Where(i => branches.Select(i => i.Id).Contains(i.BranchId)).ToList();
            var quotationsByCurrency = quotations.Where(i => i.CurrencyId.Equals(currencyId)).ToList();

            for (int i = 0; i < branches.Count(); i++)
            {
                var branch = branches[i];
                var quotation = quotationsByCurrency.FirstOrDefault(i => i.BranchId.Equals(branch.Id));
                string offer = key ? quotation.Sale : quotation.Buy;
                result.Add(new BestOffersModel(branch.BankId, branch.Bank.NameRus, branch.Id, branch.Adr, offer));
            }

            return result;
        }

        private List<BestOffers> BestOfferBuy(List<QuotationDTO> data)
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderByDescending(i => i.Buy).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchId, b.Buy));
            }

            return bestOffers;
        }

        private List<BestOffers> BestOfferSale(List<QuotationDTO> data)
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderBy(i => i.Sale).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchId, b.Sale));
            }

            return bestOffers;
        }
        #endregion

        #region CurrenciesRegion
        public string[] GetCurrenciesList(int cityId)
        {
            var branches = GetBranchesInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = _quotationService.GetData().Where(i => branchID.Contains(i.BranchId)).ToList();

            var currencies = _currencyService.GetData().Where(i => quotations.Select(i => i.CurrencyId).ToList().Contains(i.Id)).Distinct().ToArray();

            List<string> result = new List<string>();

            foreach (var c in currencies)
            {
                result.Add($"{c.Logo}  {c.NameRus}");
            }

            return result.ToArray();
        }
        #endregion

        #region UserStateService

        public UserStateDTO GetUserState(long userId) => 
            _userStateService.GetState(userId) ?? new UserStateDTO() { UserId = userId };

        public void SaveState(UserStateDTO userState)
        {
            var isExist = _userStateService.GetData().FirstOrDefault(i => i.UserId.Equals(userState.UserId));
            if (isExist != null) _userStateService.Update(userState);
            else _userStateService.Add(userState);
        }

        #endregion
    }
}
