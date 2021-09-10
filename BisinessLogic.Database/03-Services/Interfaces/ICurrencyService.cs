using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDTO>> GetData();
        Task Add(CurrencyDTO item);
    }
}
