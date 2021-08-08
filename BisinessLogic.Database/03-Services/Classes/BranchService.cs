using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
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
            if (_unitOfWork.BranchRepository.GetAll().All(branch => branch.AdrRus != item.AdrRus)) 
                _unitOfWork.BranchRepository.Add(_mapper.Map<Branch>(item));
        }

        public void Delete(BranchDTO item) =>
            _unitOfWork.BranchRepository.Delete(_mapper.Map<Branch>(item));

        public IEnumerable<BranchDTO> GetData() =>
            _mapper.Map<List<BranchDTO>>(_unitOfWork.BranchRepository.GetAll());

        public BranchDTO GetWithInclude(BranchDTO item)
        {
            var request = _unitOfWork.BranchRepository.GetWithInclude(branch => branch.AdrRus == item.AdrRus);
            return _mapper.Map<BranchDTO>(request);
        }

        public IEnumerable<BranchDTO> GetBranchInCity(int id)
        {
            var items = _unitOfWork.BranchRepository.GetAll().Where(i => i.CityId.Equals(id));
            foreach (var item in items)
            {
                item.City = _unitOfWork.CityRepository.GetWithInclude(i => i.Id.Equals(item.CityId));
                item.Bank = _unitOfWork.BankRepository.GetWithInclude(i => i.Id.Equals(item.BankId));
            }
            return _mapper.Map<List<BranchDTO>>(items);
        }

        public void Update(BranchDTO branch) =>
            _unitOfWork.BranchRepository.Update(_mapper.Map<Branch>(branch));
    }
}
