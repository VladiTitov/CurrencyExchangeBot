using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(CityDTO city)
        {
            if (_unitOfWork.CityRepository.GetAll().All(a => a.NameRus != city.NameRus))
                _unitOfWork.CityRepository.Add(_mapper.Map<City>(city));
        }

        public void Delete(CityDTO item) =>
            _unitOfWork.CityRepository.Delete(_mapper.Map<City>(item));

        public IEnumerable<CityDTO> GetData() =>
            _mapper.Map<List<CityDTO>>(_unitOfWork.CityRepository.GetAll());

        public void Update(CityDTO city) =>
            _unitOfWork.CityRepository.Update(_mapper.Map<City>(city));
    }
}
