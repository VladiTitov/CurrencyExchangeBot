using System.Collections.Generic;

namespace BisinessLogic.Database
{
    interface IPhoneService
    {
        IEnumerable<PhoneDTO> GetData();
        void Add(PhoneDTO item);
        void Update(PhoneDTO item);
        void Delete(PhoneDTO item);
    }
}
