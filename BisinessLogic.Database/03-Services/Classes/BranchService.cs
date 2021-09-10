using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(BranchDTO item)
        {
            if (await IsExist(item)) _unitOfWork.BranchRepository.Add(_mapper.Map<Branch>(item));

            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(BranchDTO item)
        {
            _unitOfWork.BranchRepository.Delete(_mapper.Map<Branch>(item));
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExist(BranchDTO item)
        {
            var data = await GetData();
            var result = data.FirstOrDefault(i => i.Name.Equals(item.Name) && i.Adr.Equals(item.Adr));
            return result == null;
        }

        public async Task Update(BranchDTO item)
        {
            _unitOfWork.BranchRepository.Update(_mapper.Map<Branch>(item));
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BranchDTO>> GetData() => 
            _mapper.Map<List<BranchDTO>>(await _unitOfWork.BranchRepository.GetAllAsync(i => i.Bank, j => j.City));

        public async Task<BranchDTO> GetWithInclude(BranchDTO item)
        {
            var items = await _unitOfWork.BranchRepository.FindAsync(i => i.Adr.Equals(item.Adr) && item.Name.Equals(item.Name));
            return _mapper.Map<BranchDTO>(items.FirstOrDefault());
        }

    }
}
