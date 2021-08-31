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

        public async Task<IEnumerable<CityDTO>> GetDataAsync()
        {
            return _mapper.Map<List<CityDTO>>(await _unitOfWork.CityRepository.GetAllAsync());
        }

        public void Add(CityDTO city)
        {
            if (_unitOfWork.CityRepository.GetAll().All(a => a.NameRus != city.NameRus))
                _unitOfWork.CityRepository.Add(_mapper.Map<City>(city));
            _unitOfWork.Save();
        }

        public void Delete(CityDTO item)
        {
            _unitOfWork.CityRepository.Delete(_mapper.Map<City>(item));
            _unitOfWork.Save();
        }
        public void Update(CityDTO city)
        {
            _unitOfWork.CityRepository.Update(_mapper.Map<City>(city));
            _unitOfWork.Save();
        }

        public IEnumerable<CityDTO> GetData() =>
            _mapper.Map<List<CityDTO>>(_unitOfWork.CityRepository.GetAll());

        public IEnumerable<City> GetDataTemp() =>
            _unitOfWork.CityRepository.GetAll();
    }
}
