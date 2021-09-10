using System.Collections.Generic;
using DataAccess.DataBaseLayer;
using SimpleInjector;
using AutoMapper;
using BusinessLogic.Database;
using BusinessLogic.Database.Classes;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.MenuStucture.Services;

namespace BusinessLogic.MenuStucture
{
    public class MenuContainer
    {
        public Container CreateContainer()
        {
            var container = new Container();

            container.Register<DataContext>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Singleton);

            container.Register<ICityService, CityService>(Lifestyle.Singleton);
            container.Register<ICityRepository, CityRepository>(Lifestyle.Singleton);

            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);
            container.Register<ICurrencyRepository, CurrencyRepository>(Lifestyle.Singleton);

            container.Register<IBankService, BankService>(Lifestyle.Singleton);
            container.Register<IBankRepository, BankRepository>(Lifestyle.Singleton);

            container.Register<IUserStateService, UserStateService>(Lifestyle.Singleton);
            container.Register<IUserStateRepository, UserStateRepository>(Lifestyle.Singleton);

            container.Register<IBranchService, BranchService>(Lifestyle.Singleton);
            container.Register<IBranchRepository, BranchRepository>(Lifestyle.Singleton);

            container.Register<IQuotationService, QuotationService>(Lifestyle.Singleton);
            container.Register<IQuotationRepository, QuotationRepository>(Lifestyle.Singleton);

            container.Register<IPhoneService, PhoneService>(Lifestyle.Singleton);
            container.Register<IPhoneRepository, PhoneRepository>(Lifestyle.Singleton);

            container.Register<IMapper>(CreateMapper, Lifestyle.Singleton);
            container.Register<GetDataFromBDService>(Lifestyle.Singleton);
            container.Verify();

            return container;
        }

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>()
            {
                new BankMappingProfile(),
                new BranchMappingProfile(),
                new CityMappingProfile(),
                new CurrencyMappingProfile(),
                new QuotationMappingProfile(),
                new PhoneMappingProfile(),
                new BaseClassMappingProfile(),
                new UserStateMappingProfile()
            }));
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
