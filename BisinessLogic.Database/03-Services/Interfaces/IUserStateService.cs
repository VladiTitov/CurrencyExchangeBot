using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface IUserStateService
    {
        Task Add(UserStateDTO item);
        UserStateDTO GetState(long userId);
        Task Update(UserStateDTO item);
        Task Delete(UserStateDTO item);
        IEnumerable<UserStateDTO> GetData();
    }
}
