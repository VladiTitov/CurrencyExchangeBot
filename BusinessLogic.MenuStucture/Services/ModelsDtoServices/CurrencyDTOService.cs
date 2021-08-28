using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsDtoServices
{
    class CurrencyDTOService
    {
        private readonly ContainerPackerService _containerPacker;

        public CurrencyDTOService() => _containerPacker = new ContainerPackerService();
        public string[] GetCurrencies(int cityId)
        {
            var branches = new BranchDTOService().GetBranchesInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = _containerPacker.GetQuotations().Where(i => branchID.Contains(i.BranchDtoId)).ToList();

            var currencies = _containerPacker.GetCurrencies().Where(i => quotations.Select(i => i.CurrencyDtoId).ToList().Contains(i.Id)).Distinct().ToArray();

            List<string> result = new List<string>();

            foreach(var c in currencies)
            {
                result.Add($"{c.Logo}  {c.NameRus}");
            }

            return result.ToArray();
        }
    }
}
