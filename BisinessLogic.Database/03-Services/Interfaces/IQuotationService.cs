using System.Collections.Generic;

namespace BisinessLogic.Database
{
    public interface IQuotationService
    {
        IEnumerable<QuotationDTO> GetData();
        void Add(QuotationDTO item);
        void Update(QuotationDTO item);
        void Delete(QuotationDTO item);
    }
}
