using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _mapper;

        public UserService(IUserRepository userRepository, IUserMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ChangePassword(int userId, string password)
        {
            var dbUser = await _userRepository.Get(userId);
            dbUser.Password = password; //todo: encrypt password
            await _userRepository.Update(dbUser);
        }

        public Task<int> Create(AddUserDto user)
        {
            var dbUser = _mapper.Map(user);
            return _userRepository.Add(dbUser);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map(users);
        }

        public Task Remove(int userId)
        {
            return _userRepository.Remove(userId);
        }

        public async Task Update(int userId, UpdateUserDto user)
        {
            var dbUser = await _userRepository.Get(userId);
            dbUser.UserName = user.UserName;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.IsAdmin = user.IsAdmin;

            await _userRepository.Update(dbUser);
        }
    }
}
