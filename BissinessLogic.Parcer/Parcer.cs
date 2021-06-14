using BisinessLogic.Database;


namespace BissinessLogic.Parcer
{
    class Parcer
    {
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly ICityService _cityService;
        private readonly ICurrencyService _currencyService;
        private readonly IPhoneService _phoneService;
        private readonly IQuotationService _quotationService;

        public Parcer(IBankService bankService,
            IBranchService branchService,
            ICityService cityService,
            ICurrencyService currencyService,
            IPhoneService phoneService,
            IQuotationService quotationService)
        {
            _bankService = bankService;
            _branchService = branchService;
            _cityService = cityService;
            _currencyService = currencyService;
            _phoneService = phoneService;
            _quotationService = quotationService;
        }


        public void Start()
        {
            //Start parser
        }
    }
}
