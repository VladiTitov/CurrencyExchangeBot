using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBankService
    {
        Task<IEnumerable<BankDTO>> GetData();
        Task Add(BankDTO item);
        Task<BankDTO> GetWithInclude(BankDTO item);
    }
}
