using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
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

        public async Task Add(CityDTO city)
        {
            if (_unitOfWork.CityRepository.GetAll().All(a => a.NameRus != city.NameRus))
                _unitOfWork.CityRepository.Add(_mapper.Map<City>(city));
            await _unitOfWork.Save();
        }

        public async Task Delete(CityDTO item)
        {
            _unitOfWork.CityRepository.Delete(_mapper.Map<City>(item));
            await _unitOfWork.Save();
        }
        public async Task Update(CityDTO city)
        {
            _unitOfWork.CityRepository.Update(_mapper.Map<City>(city));
            await _unitOfWork.Save();
        }

        public IEnumerable<CityDTO> GetData() =>
            _mapper.Map<List<CityDTO>>(_unitOfWork.CityRepository.GetAll());
    }
}
