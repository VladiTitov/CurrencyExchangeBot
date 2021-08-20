using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetData();
        Task Add(CurrencyDTO item);
        Task Update(CurrencyDTO item);
        Task Delete(CurrencyDTO item);
    }
}
