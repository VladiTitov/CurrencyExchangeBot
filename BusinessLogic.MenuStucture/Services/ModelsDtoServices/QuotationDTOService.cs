using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Constants;

namespace BusinessLogic.MenuStucture.Services.ModelsDtoServices
{
    class QuotationDTOService
    {
        private class BestOffers 
        {
            public int BranchId { get; }
            public string Offer { get; }
            public BestOffers(int branchId, string offer) => (BranchId, Offer) = (branchId, offer);
        }

        private readonly ContainerPackerService _containerPacker;

        public QuotationDTOService() => _containerPacker = new ContainerPackerService();

        private List<QuotationDTO> GetQuotations(int cityId)
        {
            var branches = _containerPacker.GetBranches().Where(i => i.CityDtoId.Equals(cityId)).ToList();
            return _containerPacker.GetQuotations().Where(i => branches.Select(j => j.Id).ToList().Contains(i.BranchDtoId)).Distinct().ToList();
        }

        public string[] GetBestOffer(bool key, int cityId, int currencyId)
        {
            List<QuotationDTO> quotations = GetQuotations(cityId).Where(i=>i.CurrencyDtoId.Equals(currencyId)).ToList();
            var bestOffers = key ? BestOfferSale(quotations) : BestOfferBuy(quotations);
            List<int> branchesId = bestOffers.Select(i => i.BranchId).ToList();
            var branches = new BranchDTOService().GetBranchesList(branchesId);
            List<string> result = new List<string>();

            for (int i = 0; i < branches.Count(); i++)
            {
                result.Add($"{MenuEmojiConstants.Bank}  {branches[i].Bank.NameRus},{MenuEmojiConstants.Location}  {branches[i].Adr},{MenuEmojiConstants.Shock}  {bestOffers[i].Offer}");
            }

            return result.ToArray();
        }

        public string[] GetQuotationByBranches(int currencyId, List<BranchDTO> branches, bool key)
        {
            List<string> result = new List<string>();

            var quotations = _containerPacker.GetQuotations()
                .Where(i => branches.Select(i => i.Id).Contains(i.BranchDtoId)).ToList();
            var quotationsByCurrency = quotations.Where(i => i.CurrencyDtoId.Equals(currencyId)).ToList();

            for (int i = 0; i < branches.Count(); i++)
            {
                var branch = branches[i];
                var quotation = quotationsByCurrency.FirstOrDefault(i => i.BranchDtoId.Equals(branch.Id));
                string offer = key ? quotation.Sale : quotation.Buy;
                result.Add($"{MenuEmojiConstants.Shock}  {offer}  {MenuEmojiConstants.Location}  {branch.Adr}");
            }

            return result.ToArray();
        }

        private List<BestOffers> BestOfferBuy(List<QuotationDTO> data)
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderByDescending(i => i.Buy).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchDtoId, b.Buy));
            }

            return bestOffers;
        }


        private List<BestOffers> BestOfferSale(List<QuotationDTO> data) 
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderBy(i => i.Sale).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchDtoId, b.Sale));
            }

            return bestOffers;
        }

    }
}
