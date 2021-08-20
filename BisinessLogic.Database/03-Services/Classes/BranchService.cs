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
            if (_unitOfWork.BranchRepository.GetAll().All(branch => !branch.Name.Equals(item.Name) || !branch.Adr.Equals(item.Adr)))
                _unitOfWork.BranchRepository.Add(_mapper.Map<Branch>(item));
            await _unitOfWork.Save();
        }

        public async Task Delete(BranchDTO item)
        {
            _unitOfWork.BranchRepository.Delete(_mapper.Map<Branch>(item));
            await _unitOfWork.Save();
        }

        public async Task Update(BranchDTO branch)
        {
            _unitOfWork.BranchRepository.Update(_mapper.Map<Branch>(branch));
            await _unitOfWork.Save();
        }

        public IEnumerable<BranchDTO> GetData() =>
            _mapper.Map<List<BranchDTO>>(_unitOfWork.BranchRepository.GetAll());

        public BranchDTO GetWithInclude(BranchDTO item)
        {
            var request = _unitOfWork.BranchRepository.GetWithInclude(branch => branch.Adr.Equals(item.Adr) && branch.Name.Equals(item.Name));
            return _mapper.Map<BranchDTO>(request);
        }

    }
}
