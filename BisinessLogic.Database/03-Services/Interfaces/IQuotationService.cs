using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface IQuotationService
    {
        IEnumerable<QuotationDTO> GetData();
        Task Add(QuotationDTO item);
        Task Update(QuotationDTO item);
        void Delete(QuotationDTO item);
        Task<bool> IsExist(QuotationDTO item);
    }
}
