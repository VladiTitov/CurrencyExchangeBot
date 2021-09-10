using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task Add(PhoneDTO phone)
        {
            if (await IsExist(phone)) _unitOfWork.PhoneRepository.Add(_mapper.Map<Phone>(phone));
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PhoneDTO>> GetData() =>
           _mapper.Map<List<PhoneDTO>>(await _unitOfWork.PhoneRepository.GetAllAsync());

        public async Task<bool> IsExist(PhoneDTO item) 
        {
            var data = await GetData();
            var result = data.FirstOrDefault(i => i.PhoneNum.Equals(item.PhoneNum));
            return result == null;
        }
    }
}
