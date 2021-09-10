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
            if (await IsExist(item)) _unitOfWork.BankRepository.Add(_mapper.Map<Bank>(item));
            await _unitOfWork.SaveAsync();
        }

        private async Task<bool> IsExist(BankDTO item)
        {
            var data = await GetData();
            return data.All(i => !i.NameRus.Equals(item.NameRus));
        }

        public async Task<IEnumerable<BankDTO>> GetData() => 
            _mapper.Map<List<BankDTO>>(await _unitOfWork.BankRepository.GetAllAsync());

        public async Task<BankDTO> GetWithInclude(BankDTO item)
        {
            var pr = await _unitOfWork.BankRepository.GetFilteredAsync(i => i.NameRus.Equals(item.NameRus));
            return _mapper.Map<BankDTO>(pr.FirstOrDefault());
        }
    }
}
