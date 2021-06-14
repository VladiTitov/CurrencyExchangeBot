using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CurrencyDTO> GetData() =>
            _mapper.Map<List<CurrencyDTO>>(_unitOfWork.CurrencyRepository.GetAll());

        public void Add(CurrencyDTO currency)
        {
            if (_unitOfWork.CurrencyRepository.GetAll().All(a => a.NameRus != currency.NameRus))
                _unitOfWork.CurrencyRepository.Add(_mapper.Map<Currency>(currency));
        }

        public void Update(CurrencyDTO currency) =>
            _unitOfWork.CurrencyRepository.Update(_mapper.Map<Currency>(currency));

        public void Delete(CurrencyDTO item) =>
            _unitOfWork.CurrencyRepository.Delete(_mapper.Map<Currency>(item));
    }
}
