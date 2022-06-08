using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface IUserMapper
    {
        User Map(AddUserDto user);

        UserDto Map(User user);

        List<UserDto> Map(List<User> users);
    }
}