using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class CurrencyDTOService
    {
        private readonly ContainerPackerService _containerPacker;

        public CurrencyDTOService() => _containerPacker = new ContainerPackerService();
        public string[] GetCurrencies(string cityName)
        {
            var cityId = _containerPacker.GetCityId(cityName);
            var branches = new BranchDTOService().GetBranchesInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = _containerPacker.GetQuotations().Where(i => branchID.Contains(i.BranchDtoId)).ToList();
            var currencies = _containerPacker.GetCurrencies().Where(i => quotations.Select(i => i.CurrencyDtoId).ToList().Contains(i.Id)).ToArray().Select(i => i.NameRus).Distinct().ToArray();
            return currencies;
        }
    }
}
