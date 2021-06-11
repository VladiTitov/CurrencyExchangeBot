namespace DataAccess.DataBaseLayer
{
    public class BankRepository : GenericRepository<Bank>, IBankRepository
    {
        public BankRepository(DataContext context) : base(context)
        {
        }
    }
}
