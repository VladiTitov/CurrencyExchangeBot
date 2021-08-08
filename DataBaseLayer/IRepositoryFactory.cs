namespace DataAccess.DataBaseLayer
{
    public interface IRepositoryFactory
    {
        ICurrencyRepository CreateCurrencyRepository();
        IBankRepository CreateBankRepository();
        ICityRepository CreateCityRepository();
        IQuotationRepository CreateQuotationRepository();
        IBranchRepository CreateBranchRepository();
        IPhoneRepository CreatePhoneRepository();
        IUserStateRepository CreateUserStateRepository();
    }
}
