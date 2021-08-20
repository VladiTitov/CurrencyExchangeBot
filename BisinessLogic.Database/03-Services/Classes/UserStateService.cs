using System.Linq;
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
            if (_unitOfWork.UserStateRepository.GetAll().All(a => a.UserId != item.UserId))
                _unitOfWork.UserStateRepository.Add(_mapper.Map<UserState>(item));
            else await Update(item);
            await _unitOfWork.Save();
        }

        public async Task Update(UserStateDTO item)
        { 
            _unitOfWork.UserStateRepository.Update(_mapper.Map<UserState>(item));
            await _unitOfWork.Save();
        }
        
        public async Task Delete(UserStateDTO item)
        {
            _unitOfWork.UserStateRepository.Delete(_mapper.Map<UserState>(item));
            await _unitOfWork.Save();
        }

        public UserStateDTO GetState(long userId)
        {
            var request = _unitOfWork.UserStateRepository.GetWithInclude(item => item.UserId.Equals(userId));
            return _mapper.Map<UserStateDTO>(request);
        }

        public bool IsExist(long userId) => GetState(userId) != null;
    }
}
