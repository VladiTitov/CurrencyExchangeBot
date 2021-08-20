using System.Collections.Generic;

namespace BusinessLogic.Database.Interfaces
{
    public interface IPhoneService
    {
        IEnumerable<PhoneDTO> GetData();
        void Add(PhoneDTO item);
        void Update(PhoneDTO item);
        void Delete(PhoneDTO item);
    }
}
