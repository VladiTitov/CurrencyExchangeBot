namespace DataAccess.DataBaseLayer
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(DataContext context) : base(context) { }
    }
}
