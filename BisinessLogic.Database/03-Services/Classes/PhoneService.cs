using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class PhoneService : IPhoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhoneService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(PhoneDTO phone)
        {
            if (_unitOfWork.PhoneRepository.GetAll().All(a => a.PhoneNum != phone.PhoneNum))
                _unitOfWork.PhoneRepository.Add(_mapper.Map<Phone>(phone));
        }

        public void Delete(PhoneDTO phone) =>
            _unitOfWork.PhoneRepository.Delete(_mapper.Map<Phone>(phone));

        public IEnumerable<PhoneDTO> GetData() =>
            _mapper.Map<List<PhoneDTO>>(_unitOfWork.PhoneRepository.GetAll());

        public void Update(PhoneDTO phone) =>
            _unitOfWork.PhoneRepository.Update(_mapper.Map<Phone>(phone));
    }
}
