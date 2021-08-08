using AutoMapper;
using DataAccess.DataBaseLayer;
using System.Linq;

namespace BusinessLogic.Database
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

        public void Add(UserStateDTO item)
        {
            if (_unitOfWork.UserStateRepository.GetAll().All(a => a.UserId != item.UserId))
                _unitOfWork.UserStateRepository.Add(_mapper.Map<UserState>(item));
        }

        public void Update(UserStateDTO item) =>
            _unitOfWork.UserStateRepository.Update(_mapper.Map<UserState>(item));

        public void Delete(UserStateDTO item) => 
            _unitOfWork.UserStateRepository.Delete(_mapper.Map<UserState>(item));

        public UserStateDTO GetState(long userId)
        {
            var request = _unitOfWork.UserStateRepository.GetWithInclude(item => item.UserId.Equals(userId));
            return _mapper.Map<UserStateDTO>(request);
        }
    }
}
