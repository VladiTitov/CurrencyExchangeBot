using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
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
