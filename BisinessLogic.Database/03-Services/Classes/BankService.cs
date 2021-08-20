using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(BankDTO item)
        {
            if (_unitOfWork.BankRepository.GetAll().All(a => a.NameRus != item.NameRus))
                _unitOfWork.BankRepository.Add(_mapper.Map<Bank>(item));
            await _unitOfWork.Save();
        }

        public async Task Delete(BankDTO item)
        {
            _unitOfWork.BankRepository.Delete(_mapper.Map<Bank>(item));
            await _unitOfWork.Save();
        }
            

        public IEnumerable<BankDTO> GetData() =>
            _mapper.Map<List<BankDTO>>(_unitOfWork.BankRepository.GetAll());

        public BankDTO GetWithInclude(BankDTO item)
        {
            var bankDb = _unitOfWork.BankRepository.GetWithInclude(bank => bank.NameRus == item.NameRus);
            return _mapper.Map<BankDTO>(bankDb);
        }

        public async Task Update(BankDTO item)
        {
            _unitOfWork.BankRepository.Update(_mapper.Map<Bank>(item));
            await _unitOfWork.Save();
        }
            
    }
}
