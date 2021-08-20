using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Database;
using BusinessLogic.Database.Classes;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.Parser.Services.Classes;
using BusinessLogic.Parser.Services.Interfaces;
using DataAccess.DataBaseLayer;
using DataAccess.SeleniumHtmlParse;
using DataAccess.SeleniumHtmlParse.Repositories.Classes;
using FluentScheduler;
using SimpleInjector;

namespace BusinessLogic.Parser
{
    class ParserJob : IJob
    {
        public void Execute()
        {
            var container = CreateContainer();
            container.GetInstance<BusinessLogic.Parser.Parser>().Start();
        }

        private static Container CreateContainer()
        {
            var container = new Container();

            container.Register<DataContext>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Singleton);

            container.Register<IBankService, BankService>(Lifestyle.Singleton);
            container.Register<IBankRepository, BankRepository>(Lifestyle.Singleton);

            container.Register<IBranchService, BranchService>(Lifestyle.Singleton);
            container.Register<IBranchRepository, BranchRepository>(Lifestyle.Singleton);

            container.Register<ICityService, CityService>(Lifestyle.Singleton);
            container.Register<ICityRepository, CityRepository>(Lifestyle.Singleton);

            container.Register<ICurrencyService, CurrencyService>(Lifestyle.Singleton);
            container.Register<ICurrencyRepository, CurrencyRepository>(Lifestyle.Singleton);

            container.Register<IPhoneService, PhoneService>(Lifestyle.Singleton);
            container.Register<IPhoneRepository, PhoneRepository>(Lifestyle.Singleton);

            container.Register<IQuotationService, QuotationService>(Lifestyle.Singleton);
            container.Register<IQuotationRepository, QuotationRepository>(Lifestyle.Singleton);

            container.Register<ICityParserRepository, CityParserRepository>(Lifestyle.Singleton);
            container.Register<ICityWebDataService, CityWebDataService>(Lifestyle.Singleton);

            container.Register<ICurrencyParserRepository, CurrencyParserRepository>(Lifestyle.Singleton);
            container.Register<ICurrencyWebDataService, CurrencyWebDataService>(Lifestyle.Singleton);

            container.Register<IBaseWebDataService, BaseWebDataService>(Lifestyle.Singleton);
            container.Register<IBaseParserRepository, BaseParserRepository>(Lifestyle.Singleton);

            container.Register<IMapper>(CreateMapper, Lifestyle.Singleton);
            container.Register<BusinessLogic.Parser.Parser>(Lifestyle.Singleton);
            container.Verify();

            return container;
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>()
            {
                new BankMappingProfile(),
                new BranchMappingProfile(),
                new CityMappingProfile(),
                new CurrencyMappingProfile(),
                new QuotationMappingProfile(),
                new PhoneMappingProfile(),
                new BaseClassMappingProfile()
            }));
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
