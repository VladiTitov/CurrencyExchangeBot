using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankDTO> GetData();
        Task<IEnumerable<BankDTO>> GetDataAsync();
        IEnumerable<Bank> GetDataTemp();
        void Add(BankDTO item);
        void Update(BankDTO item);
        void Delete(BankDTO item);
        BankDTO GetWithInclude(BankDTO item);
    }
}
