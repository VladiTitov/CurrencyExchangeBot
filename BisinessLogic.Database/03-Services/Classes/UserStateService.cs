using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Database.Interfaces;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Classes
{
    public class UserStateService : IUserStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserStateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(UserStateDTO item)
        {
            _unitOfWork.UserStateRepository.Add(_mapper.Map<UserState>(item));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(UserStateDTO item)
        { 
            _unitOfWork.UserStateRepository.Update(_mapper.Map<UserState>(item));
            await _unitOfWork.SaveAsync();
        }
        
        public async Task Delete(UserStateDTO item)
        {
            _unitOfWork.UserStateRepository.Delete(_mapper.Map<UserState>(item));
            await _unitOfWork.SaveAsync();
        }

        public UserStateDTO GetState(long userId)
        {
            var item = _unitOfWork.UserStateRepository.GetWithInclude(item => item.UserId.Equals(userId));
            return _mapper.Map<UserStateDTO>(item);
        }

        public IEnumerable<UserStateDTO> GetData()
        {
            var request = _unitOfWork.UserStateRepository.GetAll();
            return _mapper.Map<List<UserStateDTO>>(request);
        }
    }
}
