using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(BranchDTO item)
        {
            if (_unitOfWork.BranchRepository.GetAll().All(branch => branch.AdrRus != item.AdrRus)) 
                _unitOfWork.BranchRepository.Add(_mapper.Map<Branch>(item));
        }

        public void Delete(BranchDTO item) =>
            _unitOfWork.BranchRepository.Delete(_mapper.Map<Branch>(item));

        public IEnumerable<BranchDTO> GetData() =>
            _mapper.Map<List<BranchDTO>>(_unitOfWork.BranchRepository.GetAll());

        public void Update(BranchDTO branch) =>
            _unitOfWork.BranchRepository.Update(_mapper.Map<Branch>(branch));
    }
}
