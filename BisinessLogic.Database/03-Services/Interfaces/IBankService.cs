using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankDTO> GetData();
        void Add(BankDTO item);
        void Update(BankDTO item);
        void Delete(BankDTO item);
        BankDTO GetWithInclude(BankDTO item);
    }
}
