using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User Map(AddUserDto user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                IsAdmin = user.IsAdmin,
                Password = user.Password, // Todo: encrypt password
            };
        }

        public UserDto Map(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin
            };
        }

        public List<UserDto> Map(List<User> users)
        {
            return users.Select(user => Map(user)).ToList();
        }
    }
}