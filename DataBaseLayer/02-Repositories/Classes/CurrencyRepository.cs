namespace DataAccess.DataBaseLayer
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(DataContext context) : base(context) { }
    }
}
