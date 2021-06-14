using System.Collections.Generic;

namespace BisinessLogic.Database
{
    interface ICurrencyService
    {
        IEnumerable<CurrencyDTO> GetData();
        void Add(CurrencyDTO item);
        void Update(CurrencyDTO item);
        void Delete(CurrencyDTO item);
    }
}
