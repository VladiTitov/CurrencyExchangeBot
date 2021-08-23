using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class QuotationDTOService
    {
        private class BestOffers 
        {
            public int BranchId { get; set; }
            public string Offer { get; set; }
            public BestOffers(int branchId, string offer) => (BranchId, Offer) = (branchId, offer);
        }


        private readonly ContainerPackerService _containerPacker;

        public QuotationDTOService() => _containerPacker = new ContainerPackerService();

        private List<QuotationDTO> GetQuotations(int cityId)
        {
            var branches = _containerPacker.GetBranches().Where(i => i.CityDtoId.Equals(1));
            return _containerPacker.GetQuotations().Where(i => branches.Select(i => i.Id).ToList().Contains(i.Id)).Distinct().ToList();
        }

        public string[] GetBestOffer(bool key, int cityId)
        {
            List<BestOffers> bestOffers = new List<BestOffers>();
            List<QuotationDTO> quotations = GetQuotations(cityId);

            if (key) bestOffers = BestOfferBuy(quotations);
            else bestOffers = BestOfferSale(quotations);

            var branches = new BranchDTOService().GetBranchesList(bestOffers.Select(i=>i.BranchId));
            
            List<string> result = new List<string>();

            for (int i = 0; i < branches.Count(); i++)
            {
                result.Add($"{branches[i].Bank.NameRus}\n{branches[i].Adr}\n{bestOffers[i].Offer}");
            }

            return result.ToArray();
        }

        private List<BestOffers> BestOfferBuy(List<QuotationDTO> data)
        {
            List<BestOffers> bestOffers = new List<BestOffers>();
            var best = data.OrderBy(i => i.Buy).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.Id, b.Buy));
            }

            return bestOffers;
        }


        private List<BestOffers> BestOfferSale(List<QuotationDTO> data) 
        {
            List<BestOffers> bestOffers = new List<BestOffers>();
            var best = data.OrderByDescending(i => i.Sale).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.Id, b.Sale));
            }

            return bestOffers;
        }

    }
}
