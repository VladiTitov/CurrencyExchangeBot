namespace DataAccess.DataBaseLayer
{
    interface IRepositoryFactory
    {
        ICurrencyRepository CreateCurrencyRepository();
        IBankRepository CreateBankRepository();
        ICityRepository CreateCityRepository();
        IQuotationRepository CreateQuotationRepository();
        IBranchRepository CreateBranchRepository();
        IPhoneRepository CreatePhoneRepository();
    }
}
