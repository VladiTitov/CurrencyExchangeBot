using System;

namespace DataAccess.DataBaseLayer
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        private ICurrencyRepository _currencyRepository;
        private IBankRepository _bankRepository;
        private ICityRepository _cityRepository;
        private IBranchRepository _branchRepository;
        private IQuotationRepository _quotationRepository;
        private IPhoneRepository _phoneRepository;
        private bool _disposed;

        public UnitOfWork(DataContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }


        public IBankRepository BankRepository =>
            _bankRepository ??= _repositoryFactory.CreateBankRepository();

        public ICurrencyRepository CurrencyRepository =>
            _currencyRepository ??= _repositoryFactory.CreateCurrencyRepository();

        public ICityRepository CityRepository =>
            _cityRepository ??= _repositoryFactory.CreateCityRepository();

        public IBranchRepository BranchRepository =>
            _branchRepository ??= _repositoryFactory.CreateBranchRepository();

        public IQuotationRepository QuotationRepository =>
            _quotationRepository ??= _repositoryFactory.CreateQuotationRepository();

        public IPhoneRepository PhoneRepository =>
            _phoneRepository ??= _repositoryFactory.CreatePhoneRepository();

        public void Save() => _context.SaveChanges();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _branchRepository?.Dispose();
                _currencyRepository?.Dispose();
                _bankRepository?.Dispose();
                _quotationRepository?.Dispose();
                _cityRepository?.Dispose();
                _phoneRepository?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
