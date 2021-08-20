using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class CurrencyService : ICurrencyService
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

        public async Task Add(CurrencyDTO currency)
        {
            if (_unitOfWork.CurrencyRepository.GetAll().All(a => a.NameRus != currency.NameRus))
                _unitOfWork.CurrencyRepository.Add(_mapper.Map<Currency>(currency));
            await _unitOfWork.Save();
        }

        public async Task Update(CurrencyDTO currency)
        {
            _unitOfWork.CurrencyRepository.Update(_mapper.Map<Currency>(currency));
            await _unitOfWork.Save();
        }


        public async Task Delete(CurrencyDTO item)
        {
            _unitOfWork.CurrencyRepository.Delete(_mapper.Map<Currency>(item));
            await _unitOfWork.Save();
        }
        
    }
}
