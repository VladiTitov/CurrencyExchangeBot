using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankDTO> GetData();
        Task Add(BankDTO item);
        Task Update(BankDTO item);
        Task Delete(BankDTO item);
        BankDTO GetWithInclude(BankDTO item);
    }
}
