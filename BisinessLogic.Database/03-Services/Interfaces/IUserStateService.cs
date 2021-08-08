namespace BusinessLogic.Database
{
    public interface IUserStateService
    {
        void Add(UserStateDTO item);
        UserStateDTO GetState(long userId);
        void Update(UserStateDTO item);
        void Delete(UserStateDTO item);
    }
}
