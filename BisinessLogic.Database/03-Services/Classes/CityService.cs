using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetData() => 
            _mapper.Map<List<CityDTO>>(await _unitOfWork.CityRepository.GetAllAsync());

        public async Task Add(CityDTO city)
        {
            if (IsExist(city)) _unitOfWork.CityRepository.Add(_mapper.Map<City>(city));
            await _unitOfWork.SaveAsync();
        }

        public bool IsExist(CityDTO item) =>
           GetData().Result.All(i => !i.NameRus.Equals(item.NameRus));
    }
}
