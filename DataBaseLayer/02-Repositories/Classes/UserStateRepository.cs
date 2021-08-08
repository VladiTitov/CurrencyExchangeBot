namespace DataAccess.DataBaseLayer
{
    public class UserStateRepository : GenericRepository<UserState>, IUserStateRepository
    {
        public UserStateRepository(DataContext context) : base(context) { }
    }
}
