using System.Collections.Generic;

namespace BusinessLogic.Database.Interfaces
{
    public interface IQuotationService
    {
        IEnumerable<QuotationDTO> GetData();
        void Add(QuotationDTO item);
        void Update(QuotationDTO item);
        void Delete(QuotationDTO item);
    }
}
