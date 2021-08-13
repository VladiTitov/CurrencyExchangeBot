using System.Threading.Tasks;

namespace BusinessLogic.Database
{
    public interface IUserStateService
    {
        Task Add(UserStateDTO item);
        UserStateDTO GetState(long userId);
        Task Update(UserStateDTO item);
        Task Delete(UserStateDTO item);
        bool IsExist(long userId);
    }
}
