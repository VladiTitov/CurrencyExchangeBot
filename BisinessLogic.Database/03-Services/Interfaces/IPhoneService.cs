using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface IPhoneService
    {
        Task<IEnumerable<PhoneDTO>> GetData();
        Task Add(PhoneDTO item);
    }
}
