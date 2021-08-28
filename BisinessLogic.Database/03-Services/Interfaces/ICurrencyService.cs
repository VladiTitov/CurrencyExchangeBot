using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetData();
        void Add(CurrencyDTO item);
        void Update(CurrencyDTO item);
        void Delete(CurrencyDTO item);
    }
}
