namespace DataAccess.DataBaseLayer
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataContext _context;

        public RepositoryFactory(DataContext context) => _context = context;

        public ICurrencyRepository CreateCurrencyRepository() => 
            new CurrencyRepository(_context);

        public IBankRepository CreateBankRepository() =>
            new BankRepository(_context);

        public ICityRepository CreateCityRepository() => 
            new CityRepository(_context);

        public IQuotationRepository CreateQuotationRepository() => 
            new QuotationRepository(_context);

        public IBranchRepository CreateBranchRepository() => 
            new BranchRepository(_context);

        public IPhoneRepository CreatePhoneRepository() =>
            new PhoneRepository(_context);
    }
}
