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

        public async Task<IEnumerable<CurrencyDTO>> GetData() =>
           _mapper.Map<List<CurrencyDTO>>(await _unitOfWork.CurrencyRepository.GetAllAsync());

        public async Task Add(CurrencyDTO currency)
        {
            if (await IsExist(currency)) _unitOfWork.CurrencyRepository.Add(_mapper.Map<Currency>(currency));
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExist(CurrencyDTO item) 
        {
            var data = await GetData();
            var result = data.FirstOrDefault(i => i.NameRus.Equals(item.NameRus));
            return result == null;
        }
    }
}
