using System.Collections.Generic;
using System.Linq;
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

        public void Add(BranchDTO item)
        {
            if (IsExist(item)) _unitOfWork.BranchRepository.Add(_mapper.Map<Branch>(item));
            else
            {
                return;
            }
            _unitOfWork.Save();
        }

        public void Delete(BranchDTO item)
        {
            _unitOfWork.BranchRepository.Delete(_mapper.Map<Branch>(item));
            _unitOfWork.Save();
        }

        public bool IsExist(BranchDTO item) => _unitOfWork.BranchRepository.GetAll()
                .All(branch => !branch.Name.Equals(item.Name) || !branch.Adr.Equals(item.Adr));

        public void Update(BranchDTO item)
        {
            _unitOfWork.BranchRepository.Update(_mapper.Map<Branch>(item));
            _unitOfWork.Save();
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
