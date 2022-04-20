using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface IColorMapper
    {
        Color Map(AddOrUpdateColorDto color);
        ColorDto Map(Color color);
    }
}