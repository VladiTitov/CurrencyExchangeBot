using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface IQuotationService
    {
        Task<IEnumerable<QuotationDTO>> GetData();
        Task Add(QuotationDTO item);
        Task<bool> IsExist(QuotationDTO item);
    }
}
