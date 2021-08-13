using System.Collections.Generic;
using System.Threading.Tasks;

namespace BisinessLogic.Database
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetData();
        Task Add(CurrencyDTO item);
        Task Update(CurrencyDTO item);
        Task Delete(CurrencyDTO item);
    }
}
