using System;
using System.Threading.Tasks;

namespace DataAccess.DataBaseLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IBankRepository BankRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        ICityRepository CityRepository { get; }
        IBranchRepository BranchRepository { get; }
        IQuotationRepository QuotationRepository { get; }
        IPhoneRepository PhoneRepository { get; }
        IUserStateRepository UserStateRepository { get; }
        Task Save();
    }
}
